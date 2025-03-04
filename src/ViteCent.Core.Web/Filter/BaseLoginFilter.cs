#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BaseLoginFilter : ActionFilterAttribute
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));

        var httpContext = context.HttpContext;

        var json = httpContext.User.FindFirstValue(ClaimTypes.UserData);

        if (string.IsNullOrWhiteSpace(json))
        {
            context.Result = result;
            return;
        }

        var user = json.DeJson<BaseUserInfo>();

        if (user == null)
            context.Result = result;
    }
}