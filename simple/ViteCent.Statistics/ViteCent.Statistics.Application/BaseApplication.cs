#region

using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Statistics.Application;

/// <summary>
/// 基础应用服务类
/// </summary>
/// <remarks>提供统计应用服务的基础功能，包括用户信息的初始化和处理。 该类作为统计模块中所有应用服务的基础类，提供通用的用户信息处理方法。</remarks>
public static class BaseApplication
{
    /// <summary>
    /// 初始化并获取当前用户信息
    /// </summary>
    /// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前请求的上下文信息</param>
    /// <returns>返回包含用户基本信息的BaseUserInfo对象</returns>
    /// <remarks>
    /// 该方法主要完成以下功能：
    /// 1. 从HTTP请求头中获取Token信息
    /// 2. 从Claims中解析用户数据
    /// 3. 将用户数据反序列化为BaseUserInfo对象
    /// 4. 设置用户Token
    /// </remarks>
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