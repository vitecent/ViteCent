#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>
/// <param name="cache"></param>
/// <param name="configuration"></param>
/// <param name="system"></param>
/// <param name="resource"></param>
/// <param name="operation"></param>
[AttributeUsage(AttributeTargets.Method)]
public class BaseAuthFilter(IBaseCache cache, IConfiguration configuration, string system, string resource, string operation) : ActionFilterAttribute
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
        var logger = new BaseLogger(typeof(BaseAuthFilter));

        var result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));

        var httpContext = context.HttpContext;

        var token = httpContext.Request.Headers[Const.Token];

        if (string.IsNullOrWhiteSpace(token))
        {
            logger.LogInformation($"InvokeAsync {System}:{Resource}:{Operation} Not Token");
            context.Result = result;
            return;
        }

        var json = httpContext.User.FindFirstValue(ClaimTypes.UserData);

        if (string.IsNullOrWhiteSpace(json))
        {
            logger.LogInformation($"InvokeAsync {System}:{Resource}:{Operation} Not UserData");
            context.Result = result;

            return;
        }

        var user = json.DeJson<BaseUserInfo>();

        if (user == null)
        {
            logger.LogInformation($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} Not Login");
            context.Result = result;

            return;
        }

        var cahceToken = string.Empty;

        if (cache.HasKey($"User{user.Id}"))
            cache.GetString<string>($"User{user.Id}");

        if (string.IsNullOrWhiteSpace(cahceToken) || token != cahceToken)
        {
            logger.LogInformation($"{user.Name} InvokeAsync {System}:{Resource}:{Operation} Not Cache");

            context.Result = result;

            return;
        }

        var flagExpires = int.TryParse(configuration["Jwt:Expires"] ?? default!, out var expires);

        if (!flagExpires || expires < 1) expires = 24;

        cache.SetKeyExpire($"User{user.Id}", TimeSpan.FromHours(expires));
        cache.SetKeyExpire($"UserInfo{user.Id}", TimeSpan.FromHours(expires));

        if (user?.IsSuper != (int)YesNoEnum.Yes)
            if (!IsAUth(user, System, Resource, Operation))
            {
                logger.LogInformation($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} No Permission");
                context.Result = new JsonResult(new BaseResult(401, "您没有权限访问该资源"));

                return;
            }

        logger.LogInformation($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} Success");
    }

    /// <summary>
    /// </summary>
    /// <param name="user"></param>
    /// <param name="system"></param>
    /// <param name="resource"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    private bool IsAUth(BaseUserInfo? user, string system, string resource, string operation)
    {
        var auth = new List<BaseSystemInfo>();

        if (cache.HasKey($"UserInfo{user?.Id}"))
            auth = cache.GetString<List<BaseSystemInfo>>($"UserInfo{user?.Id}");

        if (auth == null) return false;

        var _system = auth?.FirstOrDefault(x => x.Code == system);

        if (_system == null) return false;

        var _resource = _system.Resources.FirstOrDefault(x => x.Code == resource);

        if (_resource == null) return false;

        var _operation = _resource.Operations.FirstOrDefault(x => x.Code == operation);

        if (_operation != null) return true;

        return false;
    }
}