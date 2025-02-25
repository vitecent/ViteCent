#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>
public class BaseLoginFilter : ActionFilterAttribute
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = (BaseLoginApi<BaseArgs, BaseResult>)context.Controller;

        if (controller?.User == null) context.Result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));
    }
}