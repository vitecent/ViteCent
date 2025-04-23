#region

using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Statistics.Application;

/// <summary>
/// 基础仓储
/// </summary>
public static class BaseApplication
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public static BaseUserInfo InitUser(this IHttpContextAccessor httpContextAccessor)
    {
        var user = new BaseUserInfo();

        var context = httpContextAccessor.HttpContext;

        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        user.Token = token;

        return user;
    }
}