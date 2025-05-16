#region

using Microsoft.AspNetCore.Http;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// Cookie操作工具类，提供Cookie的读取、设置和清除功能
/// </summary>
/// <param name="context">HTTP上下文对象，用于访问请求和响应中的Cookie</param>
public class BaseCookie(HttpContext context)
{
    /// <summary>
    /// 清除所有Cookie
    /// </summary>
    public void ClearCookie()
    {
        context.Request.Cookies.Keys.ToList().ForEach(x => ClearCookie(x));
    }

    /// <summary>
    /// 清除指定键的Cookie
    /// </summary>
    /// <param name="key">要清除的Cookie键名</param>
    public void ClearCookie(string key)
    {
        context.Response.Cookies.Delete(key);
    }

    /// <summary>
    /// 获取指定键的Cookie值
    /// </summary>
    /// <param name="key">要获取的Cookie键名</param>
    /// <returns>返回Cookie的值，如果不存在则返回默认值</returns>
    public string GetCookie(string key)
    {
        context.Request.Cookies.TryGetValue(key, out var value);

        return value ?? default!;
    }

    /// <summary>
    /// 设置Cookie，使用默认过期时间
    /// </summary>
    /// <param name="key">Cookie的键名</param>
    /// <param name="vlaue">Cookie的值</param>
    public void SetCookie(string key, string vlaue)
    {
        SetCookie(key, vlaue, default);
    }

    /// <summary>
    /// 设置Cookie，并指定过期时间
    /// </summary>
    /// <param name="key">Cookie的键名</param>
    /// <param name="vlaue">Cookie的值</param>
    /// <param name="day">过期天数，如果为默认值则不设置过期时间</param>
    public void SetCookie(string key, string vlaue, double day)
    {
        context.Response.Cookies.Append(key, vlaue, new CookieOptions { Expires = DateTime.Now.AddDays(day) });
    }
}