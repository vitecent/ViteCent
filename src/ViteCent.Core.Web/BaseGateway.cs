#region

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Register;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
///     Class BaseGateway.
/// </summary>
/// <param name="next"></param>
/// <param name="httpClient"></param>
/// <param name="serviceProvider"></param>
/// <param name="cache"></param>
public class BaseGateway(
    RequestDelegate next,
    IHttpClientFactory httpClient,
    IServiceProvider serviceProvider,
    IBaseCache cache)
{
    /// <summary>
    ///     The register
    /// </summary>
    private readonly IRegister register = serviceProvider.GetService<IRegister>() ?? default!;

    /// <summary>
    ///     Invokes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public async Task Invoke(HttpContext context)
    {
        var logger = BaseLogger.GetLogger();
        var traceingId = string.Empty;

        if (context.Request.Headers.TryGetValue(Const.TraceingId, out var value)) traceingId = value.ToString();

        if (string.IsNullOrWhiteSpace(traceingId))
        {
            traceingId = Guid.NewGuid().ToString("N");
            context.Request.Headers.Remove(Const.TraceingId);
            context.Request.Headers.TryAdd(Const.TraceingId, traceingId);
        }

        logger.Info($"Gateway TraceingId {traceingId}");

        context.Response.Headers.TryAdd(Const.TraceingId, traceingId);

        var uri = GetServiceUri(context);

        if (!string.IsNullOrWhiteSpace(uri))
        {
            var timeoutPolicy = Policy.TimeoutAsync(15);

            var retryPolicy = Policy.Handle<Exception>()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync([
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20)
                ]);

            var breakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

            var policyWrap = timeoutPolicy.WrapAsync(retryPolicy).WrapAsync(breakerPolicy);

            await policyWrap.ExecuteAsync(async () =>
            {
                logger.Info($"Gateway Url {uri}");

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

                logger.Info($"Gateway StatusCode {statusCode}");

                foreach (var header in response.Headers) context.Response.Headers[header.Key] = header.Value.ToArray();

                foreach (var header in response.Content.Headers)
                    context.Response.Headers[header.Key] = header.Value.ToArray();

                context.Response.Headers.Remove("Transfer-Encoding");

                var body = context.Response.Body;

                logger.Info($"Gateway Response {body}");

                await response.Content.CopyToAsync(body);
            });

            return;
        }

        await next(context);
    }

    /// <summary>
    ///     Gets the service URI.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>System.String.</returns>
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

        if (key == "check" && key == "gateway") return default!;

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