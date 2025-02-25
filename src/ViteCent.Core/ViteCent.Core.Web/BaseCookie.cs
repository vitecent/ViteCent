#region

using Microsoft.AspNetCore.Http;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
/// <remarks></remarks>
/// <param name="context"></param>
public class BaseCookie(HttpContext context)
{
    /// <summary>
    /// </summary>
    public void ClearCookie()
    {
        context.Request.Cookies.Keys.ToList().ForEach(x => ClearCookie(x));
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    public void ClearCookie(string key)
    {
        context.Response.Cookies.Delete(key);
    }

    /// <summary>
    ///     /
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetCookie(string key)
    {
        context.Request.Cookies.TryGetValue(key, out var value);

        return value ?? default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="vlaue"></param>
    public void SetCookie(string key, string vlaue)
    {
        SetCookie(key, vlaue, default);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="vlaue"></param>
    /// <param name="day">天数</param>
    public void SetCookie(string key, string vlaue, double day)
    {
        context.Response.Cookies.Append(key, vlaue, new CookieOptions { Expires = DateTime.Now.AddDays(day) });
    }
}