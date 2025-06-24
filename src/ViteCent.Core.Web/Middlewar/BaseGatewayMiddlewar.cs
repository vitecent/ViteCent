#region

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Register;

#endregion

namespace ViteCent.Core.Web.Middlewar;

/// <summary>
/// API网关中间件，用于处理请求转发和负载均衡
/// </summary>
/// <param name="next">请求处理管道中的下一个中间件</param>
/// <param name="httpClient">HTTP客户端工厂，用于创建HTTP请求</param>
/// <param name="cache">缓存器，用于存储和获取服务注册信息</param>
/// <param name="configuration">配置接口，用于获取系统配置信息</param>
public class BaseGatewayMiddlewar(
    RequestDelegate next,
    IHttpClientFactory httpClient,
    IBaseCache cache,
    IConfiguration configuration)
{
    /// <summary>
    /// 处理HTTP请求的方法
    /// </summary>
    /// <param name="context">当前HTTP请求的上下文信息</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var logger = new BaseLogger(typeof(BaseGatewayMiddlewar));
        var traceingId = string.Empty;

        if (context.Request.Headers.TryGetValue(BaseConst.TraceingId, out var value)) traceingId = value.ToString();

        if (string.IsNullOrWhiteSpace(traceingId))
        {
            traceingId = Guid.NewGuid().ToString("N");
            context.Request.Headers.Remove(BaseConst.TraceingId);
            context.Request.Headers.TryAdd(BaseConst.TraceingId, traceingId);
        }

        logger.LogInformation($"Gateway TraceingId {traceingId}");

        context.Response.Headers.TryAdd(BaseConst.TraceingId, traceingId);

        var uri = GetServiceUri(context);

        if (!string.IsNullOrWhiteSpace(uri))
        {
            await BasePolicy<Task>.ExecuteAsync(async () =>
            {
                logger.LogInformation($"Gateway Url {uri}");

                var request = new HttpRequestMessage
                {
                    Method = new HttpMethod(context.Request.Method),
                    RequestUri = new Uri(uri),
                    Content = new StreamContent(context.Request.Body)
                };

                foreach (var header in context.Request.Headers)
                    if (!request.Headers.TryAddWithoutValidation(header.Key, [.. header.Value]))
                        request.Content?.Headers.TryAddWithoutValidation(header.Key, [.. header.Value]);

                using var response = await httpClient.CreateClient().SendAsync(request);

                var statusCode = (int)response.StatusCode;

                context.Response.StatusCode = statusCode;

                logger.LogInformation($"Gateway StatusCode {statusCode}");

                foreach (var header in response.Headers) context.Response.Headers[header.Key] = header.Value.ToArray();

                foreach (var header in response.Content.Headers)
                    context.Response.Headers[header.Key] = header.Value.ToArray();

                context.Response.Headers.Remove("Transfer-Encoding");

                var body = context.Response.Body;

                logger.LogInformation($"Gateway Response {body}");

                await response.Content.CopyToAsync(body);

                return Task.CompletedTask;
            });

            return;
        }

        await next(context);
    }

    /// <summary>
    /// 根据请求上下文获取目标服务的URI
    /// </summary>
    /// <param name="context">当前HTTP请求的上下文信息</param>
    /// <returns>目标服务的URI，如果未找到匹配的服务则返回null</returns>
    private string GetServiceUri(HttpContext context)
    {
        var baseUri = new Uri(context.Request.GetDisplayUrl());

        var uri = string.Empty;

        var pathAndQuery = baseUri.PathAndQuery.ToLower();

        var key =
            pathAndQuery.Split("/", StringSplitOptions.RemoveEmptyEntries).Where(x => x != "api").FirstOrDefault() ??
            default!;

        if (string.IsNullOrWhiteSpace(key))
            return default!;

        var ignores = configuration["Ignores"]?.Split(",", StringSplitOptions.RemoveEmptyEntries)?.ToList() ?? [];

        if (ignores.Any(x => x.Equals(key))) return default!;

        var services = new Dictionary<string, List<ServiceConfig>>();

        if (cache.HasKey(BaseConst.RegistServices))
            services = cache.GetString<Dictionary<string, List<ServiceConfig>>>(BaseConst.RegistServices);

        if (services.TryGetValue(key, out var list))
        {
            var microService = BaseService.GetServiceRandom(list);

            if (microService is not null)
            {
                uri = $"http://{microService.Address}:{microService.Port}{pathAndQuery}";

                var replace = $"/{key}";

                var index = uri.IndexOf(replace);

                if (index != -1)
                    uri = uri.Remove(index, replace.Length);

                if (microService.IsHttps) uri = uri.Replace("http://", "https://");
            }

            return uri;
        }

        return default!;
    }
}