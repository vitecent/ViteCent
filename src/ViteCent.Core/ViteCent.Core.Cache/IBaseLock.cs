namespace ViteCent.Core.Cache;

/// <summary>
///     Interface ILock
/// </summary>
public interface IBaseLock
{
    /// <summary>
    ///     Locks the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="time">The time.</param>
    /// <returns>result</returns>
    bool Lock(string key, TimeSpan time);

    /// <summary>
    ///     Releases this instance.
    /// </summary>
    void Release();
}