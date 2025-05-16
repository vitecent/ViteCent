#region

using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// 定义服务调用的基本契约接口
/// </summary>
/// <typeparam name="Args">请求参数类型，必须继承自BaseArgs基类</typeparam>
/// <typeparam name="Result">返回结果类型，必须继承自BaseResult基类</typeparam>
public interface IBaseInvoke<Args, Result> where Args : BaseArgs where Result : BaseResult
{
    /// <summary>
    /// 执行GET方法的异步服务调用
    /// </summary>
    /// <param name="service">服务名称</param>
    /// <param name="api">API接口名称</param>
    /// <param name="token">认证令牌，默认为空字符串</param>
    /// <returns>返回泛型Result类型的调用结果</returns>
    Task<Result> InvokeGetMethodAsync(string service, string api, string token = "");

    /// <summary>
    /// 执行POST方法的异步服务调用
    /// </summary>
    /// <param name="service">服务名称</param>
    /// <param name="api">API接口名称</param>
    /// <param name="args">请求参数对象</param>
    /// <param name="token">认证令牌，默认为空字符串</param>
    /// <returns>返回泛型Result类型的调用结果</returns>
    Task<Result> InvokePostAsync(string service, string api, Args args, string token = "");
}