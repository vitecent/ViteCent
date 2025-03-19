#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>
/// <param name="system"></param>
/// <param name="resource"></param>
/// <param name="operation"></param>
[AttributeUsage(AttributeTargets.Method)]
public class BaseAuthFilter(string system, string resource, string operation) : ActionFilterAttribute
{
    /// <summary>
    /// </summary>
    public string Operation { get; set; } = operation;

    /// <summary>
    /// </summary>
    public string Resource { get; set; } = resource;

    /// <summary>
    /// </summary>
    public string System { get; set; } = system;

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var logger = BaseLogger.GetLogger(typeof(BaseAuthFilter));

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
        {
            logger.Info($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} Not Login");
            context.Result = result;

            return;
        }

        if (user?.IsSuper != (int)YesNoEnum.Yes)
            if (!IsAUth(user, System, Resource, Operation))
            {
                logger.Info($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} No Permission");
                context.Result = new JsonResult(new BaseResult(401, "您没有权限访问该资源"));

                return;
            }

        logger.Info($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} Success");
    }

    /// <summary>
    /// </summary>
    /// <param name="user"></param>
    /// <param name="system"></param>
    /// <param name="resource"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    private static bool IsAUth(BaseUserInfo? user, string system, string resource, string operation)
    {
        var _system = user?.Auth?.FirstOrDefault(x => x.Code == system);

        if (_system == null) return false;

        var _resource = _system.Resources.FirstOrDefault(x => x.Code == resource);

        if (_resource == null) return false;

        var _operation = _resource.Operations.FirstOrDefault(x => x.Code == operation);

        if (_operation != null) return true;

        return false;
    }
}