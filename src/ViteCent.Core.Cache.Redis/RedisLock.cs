namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// </summary>
/// <param name="configuration"></param>
public class RedisLock(string configuration) : IBaseLock
{
    /// <summary>
    /// </summary>
    private readonly RedisCache redis = new(configuration, 1);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool Lock(string key, TimeSpan time)
    {
        if (!string.IsNullOrWhiteSpace(key)) return false;

        return !redis.HasKey(key) && redis.SetString(key, string.Empty, time);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    public void Release(string key)
    {
        if (!redis.HasKey(key)) return;

        redis.DeleteKey(key);
    }
}