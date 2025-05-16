#region

using System.Security.Claims;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Api;

/// <summary>
/// 登录验证API基类，提供Token获取和用户信息解析功能
/// </summary>
/// <typeparam name="Args">API请求参数类型，必须继承自BaseArgs</typeparam>
/// <typeparam name="Result">API返回结果类型，必须继承自BaseResult</typeparam>
public abstract class BaseLoginApi<Args, Result> : BaseApi<Args, Result>
    where Args : BaseArgs
    where Result : BaseResult
{
    /// <summary>
    /// 初始化登录验证API基类的新实例
    /// </summary>
    public BaseLoginApi()
    {
    }

    /// <summary>
    /// 获取当前请求的Token值
    /// </summary>
    /// <returns>请求头中的Token字符串，如果不存在则返回null</returns>
    public string Token
    {
        get
        {
            var token = HttpContext.Request.Headers[BaseConst.Token];

            if (!string.IsNullOrWhiteSpace(token)) return token.ToString();

            return default!;
        }
    }

    /// <summary>
    /// 获取当前登录用户信息
    /// </summary>
    /// <returns>当前登录用户的BaseUserInfo对象，如果未登录则返回空的BaseUserInfo实例</returns>
    public new BaseUserInfo User
    {
        get
        {
            var json = base.User.FindFirstValue(ClaimTypes.UserData);

            if (string.IsNullOrWhiteSpace(json)) return default!;

            return json?.DeJson<BaseUserInfo>() ?? new BaseUserInfo();
        }
    }
}