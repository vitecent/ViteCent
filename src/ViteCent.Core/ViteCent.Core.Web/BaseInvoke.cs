#region

using Dapr.Client;
using Microsoft.Extensions.Configuration;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Register;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public class BaseInvoke<Args, Result>
    where Args : BaseArgs
    where Result : BaseResult
{
    /// <summary>
    /// </summary>
    private readonly IBaseCache cache;

    /// <summary>
    /// </summary>
    private readonly IConfiguration configuration;

    /// <summary>
    /// </summary>
    private readonly DaprClient dapr;

    /// <summary>
    /// </summary>
    public BaseInvoke()
    {
        var context = BaseHttpContext.Context;
        cache = context.RequestServices.GetService(typeof(IBaseCache)) as IBaseCache ?? default!;
        dapr = context.RequestServices.GetService(typeof(DaprClient)) as DaprClient ?? default!;
        configuration = context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration ?? default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="api"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Result> InvokeGetMethodAsync(string service, string api, string token = "")
    {
        if (dapr != null) return await InvokeDaprMethodAsync(HttpMethod.Get, service, api, default!, token);

        return await InvokeHttpMethod(HttpMethod.Get, service, api, default!, token);
    }

    /// <summary>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="api"></param>
    /// <param name="args"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Result> InvokePostAsync(string service, string api, Args args, string token = "")
    {
        var isDapr = configuration["Environment"] ?? default!;

        if (isDapr == "Dapr") return await InvokeDaprMethodAsync(HttpMethod.Post, service, api, args, token);

        return await InvokeHttpMethod(HttpMethod.Post, service, api, args, token);
    }

    /// <summary>
    /// </summary>
    /// <param name="method"></param>
    /// <param name="service"></param>
    /// <param name="api"></param>
    /// <param name="args"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<Result> InvokeDaprMethodAsync(HttpMethod method, string service, string api, Args args,
        string token)
    {
        var request = dapr.CreateInvokeMethodRequest<Args>(method, service, api, null, args);

        if (!string.IsNullOrWhiteSpace(token)) request.Headers.Add(Const.Token, token);

        var result = await dapr.InvokeMethodAsync<Result>(request);

        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="method"></param>
    /// <param name="service"></param>
    /// <param name="api"></param>
    /// <param name="args"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<Result> InvokeHttpMethod(HttpMethod method, string service, string api, Args args, string token)
    {
        var uri = string.Empty;

        var services = cache.GetString<Dictionary<string, List<ServiceConfig>>>(Const.RegistServices);

        if (services.TryGetValue(service.ToLower(), out var list))
        {
            var microService = BaseService.GetServiceRandom(list);

            if (microService != null)
            {
                uri = $"http://{microService.Address}:{microService.Port}{api.Replace($"/{service}", "")}";

                if (microService.IsHttps) uri = uri.Replace("http://", "https://");
            }
        }

        if (method == HttpMethod.Post) return await new BaseNet<Result>().PostAsync(uri, args, token);

        return await new BaseNet<Result>().GetAsync(uri, token);
    }
}