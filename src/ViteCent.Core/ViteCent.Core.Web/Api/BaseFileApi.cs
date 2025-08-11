#region

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Api;

/// <summary>
/// 文件上传API的基类，提供文件处理的基础功能
/// </summary>
/// <typeparam name="Args">文件参数类型，必须继承自IFormFile接口</typeparam>
/// <typeparam name="Result">返回结果类型，必须继承自BaseResult类</typeparam>
public abstract class BaseFileApi<Args, Result> : ControllerBase
    where Args : IFormFile
    where Result : BaseResult
{
    /// <summary>
    /// 处理文件上传的方法
    /// </summary>
    /// <param name="file">上传的文件对象，包含文件内容和元数据</param>
    /// <returns>返回处理结果，类型为Result</returns>
    public abstract Task<Result> InvokeAsync(Args file);
}