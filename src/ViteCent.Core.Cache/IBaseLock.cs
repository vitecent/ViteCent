namespace ViteCent.Core.Cache;

/// <summary>
/// </summary>
public interface IBaseLock
{
    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    bool Lock(string key, TimeSpan time);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    void Release(string key);
}