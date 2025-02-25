#region

using System.Security.Claims;
using ViteCent.Core.Cache;
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
    private readonly IBaseCache cache;

    /// <summary>
    /// </summary>
    public BaseLoginApi()
    {
        var context = BaseHttpContext.Context;

        cache = context.RequestServices.GetService(typeof(IBaseCache)) as IBaseCache ?? default!;
    }

    /// <summary>
    /// </summary>
    public string Token
    {
        get
        {
            var token = HttpContext.Request.Headers[Const.Token];

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

            if (!string.IsNullOrWhiteSpace(json)) return json.DeJson<BaseUserInfo>();

            return default!;
        }
    }
}