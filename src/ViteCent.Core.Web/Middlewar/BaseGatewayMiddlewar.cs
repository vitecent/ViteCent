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
/// </summary>
/// <param name="next"></param>
/// <param name="httpClient"></param>
/// <param name="cache"></param>
/// <param name="configuration"></param>
public class BaseGatewayMiddlewar(
    RequestDelegate next,
    IHttpClientFactory httpClient,
    IBaseCache cache,
    IConfiguration configuration)
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        var logger = new BaseLogger(typeof(BaseGatewayMiddlewar));
        var traceingId = string.Empty;

        if (context.Request.Headers.TryGetValue(Const.TraceingId, out var value)) traceingId = value.ToString();

        if (string.IsNullOrWhiteSpace(traceingId))
        {
            traceingId = Guid.NewGuid().ToString("N");
            context.Request.Headers.Remove(Const.TraceingId);
            context.Request.Headers.TryAdd(Const.TraceingId, traceingId);
        }

        logger.LogInformation($"Gateway TraceingId {traceingId}");

        context.Response.Headers.TryAdd(Const.TraceingId, traceingId);

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
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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

        if (cache.HasKey(Const.RegistServices))
            services = cache.GetString<Dictionary<string, List<ServiceConfig>>>(Const.RegistServices);

        if (services.TryGetValue(key, out var list))
        {
            var microService = BaseService.GetServiceRandom(list);

            if (microService != null)
            {
                uri = $"http://{microService.Address}:{microService.Port}{pathAndQuery.Replace($"/{key}", "")}";

                if (microService.IsHttps) uri = uri.Replace("http://", "https://");
            }

            return uri;
        }

        return default!;
    }
}