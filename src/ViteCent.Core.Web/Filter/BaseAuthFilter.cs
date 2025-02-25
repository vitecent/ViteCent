#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>
/// <remarks></remarks>
/// <param name="system"></param>
/// <param name="resource"></param>
/// <param name="operation"></param>
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
    ///     权限过滤
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = (BaseLoginApi<BaseArgs, BaseResult>)context.Controller;
        var logger = BaseLogger.GetLogger();

        var user = controller?.User;

        if (user is null)
        {
            logger.Info($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} No Login");
            context.Result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));

            return;
        }

        if (user?.IsSuperAdmin == (int)YesNoEnum.No)
            if (!IsAUth(user, System, Resource, Operation))
            {
                logger.Info($"{user.Name} InvokeAsync {System}:{Resource}:{Operation} No AUth");
                context.Result = new JsonResult(new BaseResult(401, "您没有权限访问该资源"));

                return;
            }

        logger.Info($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} OK");
    }

    /// <summary>
    /// </summary>
    /// <param name="user"></param>
    /// <param name="system"></param>
    /// <param name="resource"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    private static bool IsAUth(BaseUserInfo user, string system, string resource, string operation)
    {
        var _system = user.Auth.FirstOrDefault(x => x.Code == system);
        if (_system != null)
        {
            var _resource = _system.Resources.FirstOrDefault(x => x.Code == resource);
            if (_resource != null)
            {
                var _operation = _resource.Operations.FirstOrDefault(x => x.Code == operation);
                if (_operation != null) return true;
            }
        }

        return false;
    }
}