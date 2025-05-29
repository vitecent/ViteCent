#region

using Dapr.Client;
using Microsoft.Extensions.Configuration;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Register;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// 服务调用基类，提供HTTP和Dapr两种方式的微服务调用功能
/// </summary>
/// <typeparam name="Args">请求参数类型，必须继承自BaseArgs</typeparam>
/// <typeparam name="Result">返回结果类型，必须继承自BaseResult</typeparam>
/// <param name="cache">缓存服务接口，用于获取服务注册信息</param>
/// <param name="configuration">配置服务接口，用于获取环境配置</param>
/// <param name="dapr">Dapr客户端，用于Dapr方式的服务调用</param>
public class BaseInvoke<Args, Result>(IBaseCache cache, IConfiguration configuration, DaprClient dapr)
    : IBaseInvoke<Args, Result> where Args : BaseArgs
    where Result : BaseResult
{
    /// <summary>
    /// 缓存服务实例
    /// </summary>
    private readonly IBaseCache cache = cache;

    /// <summary>
    /// 配置服务实例
    /// </summary>
    private readonly IConfiguration configuration = configuration;

    /// <summary>
    /// Dapr客户端实例
    /// </summary>
    private readonly DaprClient dapr = dapr;

    /// <summary>
    /// 异步执行GET方法调用
    /// </summary>
    /// <param name="service">目标服务名称</param>
    /// <param name="api">API接口路径</param>
    /// <param name="token">认证令牌，可选参数</param>
    /// <returns>返回调用结果，类型为Result</returns>
    public async Task<Result> InvokeGetMethodAsync(string service, string api, string token = "")
    {
        if (dapr is not null) return await InvokeDaprMethodAsync(HttpMethod.Get, service, api, default!, token);

        return await InvokeHttpMethod(HttpMethod.Get, service, api, default!, token);
    }

    /// <summary>
    /// 异步执行POST方法调用
    /// </summary>
    /// <param name="service">目标服务名称</param>
    /// <param name="api">API接口路径</param>
    /// <param name="args">请求参数</param>
    /// <param name="token">认证令牌，可选参数</param>
    /// <returns>返回调用结果，类型为Result</returns>
    public async Task<Result> InvokePostAsync(string service, string api, Args args, string token = "")
    {
        var isDapr = configuration["Environment"] ?? default!;

        if (isDapr == "Dapr") return await InvokeDaprMethodAsync(HttpMethod.Post, service, api, args, token);

        return await InvokeHttpMethod(HttpMethod.Post, service, api, args, token);
    }

    /// <summary>
    /// 使用Dapr方式调用微服务
    /// </summary>
    /// <param name="method">HTTP请求方法</param>
    /// <param name="service">目标服务名称</param>
    /// <param name="api">API接口路径</param>
    /// <param name="args">请求参数</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回服务调用结果</returns>
    private async Task<Result> InvokeDaprMethodAsync(HttpMethod method, string service, string api, Args args,
        string token)
    {
        var request = dapr.CreateInvokeMethodRequest<Args>(method, service, api, null, args);

        if (!string.IsNullOrWhiteSpace(token)) request.Headers.Add(BaseConst.Token, token);

        var result = await dapr.InvokeMethodAsync<Result>(request);

        return result;
    }

    /// <summary>
    /// 使用HTTP方式调用微服务
    /// </summary>
    /// <param name="method">HTTP请求方法</param>
    /// <param name="service">目标服务名称</param>
    /// <param name="api">API接口路径</param>
    /// <param name="args">请求参数</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回服务调用结果，如果服务不存在则返回错误信息</returns>
    private async Task<Result> InvokeHttpMethod(HttpMethod method, string service, string api, Args args, string token)
    {
        var uri = string.Empty;

        var services = new Dictionary<string, List<ServiceConfig>>();

        if (cache.HasKey(BaseConst.RegistServices))
            services = cache.GetString<Dictionary<string, List<ServiceConfig>>>(BaseConst.RegistServices);

        if (services.TryGetValue(service.ToLower(), out var list))
        {
            var microService = BaseService.GetServiceRandom(list);

            if (microService is not null)
            {
                uri = $"http://{microService.Address}:{microService.Port}{api.Replace($"/{service}", "")}";

                if (microService.IsHttps) uri = uri.Replace("http://", "https://");
            }
        }

        if (list?.Count == 0)
            return (Result)new BaseResult(500, $"服务{service}不存在");

        if (string.IsNullOrWhiteSpace(uri))
            return (Result)new BaseResult(500, $"服务{service}不存在");

        if (method == HttpMethod.Post)
            return await new BaseHttpClient<Result>().PostAsync(uri, args, token);

        return await new BaseHttpClient<Result>().GetAsync(uri, token);
    }
}