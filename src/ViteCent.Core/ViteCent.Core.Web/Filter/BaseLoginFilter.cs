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
/// 基础登录验证过滤器
/// </summary>
/// <remarks>用于验证用户的登录状态，检查Token的有效性，并自动续期登录状态。 可以应用于Controller类或Action方法上。</remarks>
/// <param name="cache">缓存器，用于存储和验证用户Token</param>
/// <param name="configuration">配置接口，用于获取JWT过期时间配置</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BaseLoginFilter(IBaseCache cache, IConfiguration configuration) : ActionFilterAttribute
{
    /// <summary>
    /// 在Action执行之前进行登录验证
    /// </summary>
    /// <param name="context">Action执行上下文，包含HTTP请求信息和用户认证信息</param>
    /// <remarks>
    /// 验证流程：
    /// 1. 检查请求头中是否包含Token
    /// 2. 验证用户信息是否存在
    /// 3. 验证Token是否与缓存中的Token匹配
    /// 4. 如果验证通过，自动续期Token
    /// </remarks>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));

        var httpContext = context.HttpContext;

        var token = httpContext.Request.Headers[BaseConst.Token];

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

        if (user is null)
        {
            context.Result = result;
            return;
        }

        var cahceToken = string.Empty;

        if (cache.HasKey($"User{user.Id}"))
            cahceToken = cache.GetString<string>($"User{user.Id}");

        if (string.IsNullOrWhiteSpace(cahceToken) || token != cahceToken)
        {
            context.Result = result;
        }
        else
        {
            var flagExpires = int.TryParse(configuration["Jwt:Expires"] ?? default!, out var expires);

            if (!flagExpires || expires < 1) expires = 24;

            cache.SetKeyExpire($"User{user.Id}", TimeSpan.FromHours(expires));
            cache.SetKeyExpire($"UserInfo{user.Id}", TimeSpan.FromHours(expires));
        }
    }
}