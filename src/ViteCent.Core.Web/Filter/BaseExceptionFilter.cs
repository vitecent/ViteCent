#region

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Text.Json;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// 全局异常处理过滤器
/// </summary>
/// <remarks>
/// 用于捕获和处理Web API中未经处理的异常，提供统一的异常处理机制
/// 将异常信息记录到日志系统，并返回标准化的错误响应
/// </remarks>
public class BaseExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// 处理异常事件
    /// </summary>
    /// <param name="context">异常上下文，包含异常信息和HTTP请求/响应上下文</param>
    /// <remarks>
    /// 1. 记录异常详细信息到日志系统
    /// 2. 构造统一的错误响应格式
    /// 3. 将错误信息写入HTTP响应
    /// </remarks>
    public async void OnException(ExceptionContext context)
    {
        var logger = new BaseLogger(typeof(BaseExceptionFilter));
        logger.LogError(context.Exception, context.Exception.Message);

        var result = new BaseResult(500, context.Exception.Message);

        await context.HttpContext.Response.WriteAsJsonAsync(result, JsonSerializerOptions.Web);

        context.ExceptionHandled = true;
    }
}