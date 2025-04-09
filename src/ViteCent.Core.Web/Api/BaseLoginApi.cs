#region

using System.Security.Claims;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Api;

/// <summary>
/// </summary>
/// <typeparam name="Args"></typeparam>
/// <typeparam name="Result"></typeparam>
public abstract class BaseLoginApi<Args, Result> : BaseApi<Args, Result>
    where Args : BaseArgs
    where Result : BaseResult
{
    /// <summary>
    /// </summary>
    public BaseLoginApi()
    {
    }

    /// <summary>
    /// </summary>
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
    /// </summary>
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