#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Api;

/// <summary>
/// API控制器的基类，提供基础的请求处理功能
/// </summary>
/// <typeparam name="Args">请求参数类型，必须继承自BaseArgs类</typeparam>
/// <typeparam name="Result">返回结果类型，必须继承自BaseResult类</typeparam>
public abstract class BaseListApi<Args, Result> : ControllerBase
    where Args : IEnumerable<BaseArgs>
    where Result : BaseResult
{
    /// <summary>
    /// 处理请求的方法
    /// </summary>
    /// <param name="args">请求参数对象，包含所需的业务数据</param>
    /// <returns>返回处理结果，类型为Result</returns>
    public abstract Task<Result> InvokeAsync(Args args);
}