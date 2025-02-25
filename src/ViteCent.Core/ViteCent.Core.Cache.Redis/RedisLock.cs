namespace ViteCent.Core.Cache.Redis;

/// <summary>
///     Class RedisLock. Implements the <see cref="ViteCent.Core.Cache.IBaseLock" />
/// </summary>
/// <seealso cref="ViteCent.Core.Cache.IBaseLock" />
/// <param name="configuration"></param>
public class RedisLock(string configuration) : IBaseLock
{
    /// <summary>
    ///     The redis
    /// </summary>
    private readonly RedisCache redis = new(configuration, 1);

    /// <summary>
    ///     The key
    /// </summary>
    private string key = string.Empty;

    /// <summary>
    ///     Locks the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="time">The time.</param>
    /// <returns>result</returns>
    public bool Lock(string key, TimeSpan time)
    {
        if (string.IsNullOrWhiteSpace(this.key))
        {
            this.key = key;

            if (!redis.HasKey(key)) return redis.SetString(key, string.Empty, time);
        }

        return false;
    }

    /// <summary>
    ///     Releases this instance.
    /// </summary>
    public void Release()
    {
        if (redis.HasKey(key))
            if (redis.DeleteKey(key))
                key = default!;
    }
}