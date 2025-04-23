#region

using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>
public class BaseExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public async void OnException(ExceptionContext context)
    {
        var logger = new BaseLogger(typeof(BaseExceptionFilter));
        logger.LogError(context.Exception, context.Exception.Message);

        var result = new BaseResult(500, context.Exception.Message);

        await context.HttpContext.Response.WriteAsJsonAsync(result, JsonSerializerOptions.Web);

        context.ExceptionHandled = true;
    }
}