#region

using Microsoft.AspNetCore.Http;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// Session会话管理工具类，提供会话数据的存取和清理功能
/// </summary>
/// <param name="context">HTTP上下文对象，用于访问当前请求的Session</param>
public class BaseSession(HttpContext context)
{
    /// <summary>
    /// 清除所有会话数据
    /// </summary>
    public void ClearSession()
    {
        context.Session.Keys.ToList().ForEach(x => ClearSession(x));
    }

    /// <summary>
    /// 清除指定键的会话数据
    /// </summary>
    /// <param name="key">要清除的会话数据键名</param>
    public void ClearSession(string key)
    {
        context.Session.Remove(key);
    }

    /// <summary>
    /// 获取指定键的会话数据值
    /// </summary>
    /// <param name="key">要获取的会话数据键名</param>
    /// <returns>返回会话中存储的字符串值，如果键不存在则返回默认值</returns>
    public string GetSession(string key)
    {
        return context.Session.GetString(key) ?? default!;
    }

    /// <summary>
    /// 设置会话数据的键值对
    /// </summary>
    /// <param name="key">要设置的会话数据键名</param>
    /// <param name="value">要存储的会话数据值</param>
    public void SetSession(string key, string value)
    {
        context.Session.SetString(key, value);
    }
}