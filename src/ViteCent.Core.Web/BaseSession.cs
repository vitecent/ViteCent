#region

using Microsoft.AspNetCore.Http;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
/// <remarks></remarks>
/// <param name="context"></param>
public class BaseSession(HttpContext context)
{
    /// <summary>
    /// </summary>
    public void ClearSession()
    {
        context.Session.Keys.ToList().ForEach(x => ClearSession(x));
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    public void ClearSession(string key)
    {
        context.Session.Remove(key);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetSession(string key)
    {
        return context.Session.GetString(key) ?? default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetSession(string key, string value)
    {
        context.Session.SetString(key, value);
    }
}