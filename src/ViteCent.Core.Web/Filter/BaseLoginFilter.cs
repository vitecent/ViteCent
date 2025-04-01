#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// </summary>

/// <param name="cache"></param>
/// <param name="configuration"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BaseLoginFilter(IBaseCache cache, IConfiguration configuration) : ActionFilterAttribute
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));

        var httpContext = context.HttpContext;

        var token = httpContext.Request.Headers[Const.Token];

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Result = result;
            return;
        }

        var json = httpContext.User.FindFirstValue(ClaimTypes.UserData);

        if (string.IsNullOrWhiteSpace(json))
        {
            context.Result = result;
            return;
        }

        var user = json.DeJson<BaseUserInfo>();

        if (user == null)
        {
            context.Result = result;
            return;
        }

        var cahceToken = string.Empty;

        if (cache.HasKey($"User{user.Id}"))
            cahceToken = cache.GetString<string>($"User{user.Id}");

        if (string.IsNullOrWhiteSpace(cahceToken) || token != cahceToken)
            context.Result = result;
        else
        {
            var flagExpires = int.TryParse(configuration["Jwt:Expires"] ?? default!, out var expires);

            if (!flagExpires || expires < 1) expires = 24;

            cache.SetKeyExpire($"User{user.Id}", TimeSpan.FromHours(expires));
            cache.SetKeyExpire($"UserInfo{user.Id}", TimeSpan.FromHours(expires));
        }
    }
}