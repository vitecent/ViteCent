namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// Redis分布式锁实现类，提供基于Redis的分布式锁功能
/// </summary>
/// <param name="configuration">Redis连接配置字符串</param>
public class RedisLock(string configuration) : IBaseLock
{
    /// <summary>
    /// Redis缓存实例，用于实现分布式锁的底层操作
    /// </summary>
    private readonly RedisCache redis = new(configuration, 1);

    /// <summary>
    /// 尝试获取分布式锁
    /// </summary>
    /// <param name="key">锁的唯一标识键</param>
    /// <param name="time">锁的过期时间</param>
    /// <returns>获取锁成功返回true，否则返回false</returns>
    public bool Lock(string key, TimeSpan time)
    {
        if (!string.IsNullOrWhiteSpace(key)) return false;

        return !redis.HasKey(key) && redis.SetString(key, string.Empty, time);
    }

    /// <summary>
    /// 释放分布式锁
    /// </summary>
    /// <param name="key">要释放的锁的唯一标识键</param>
    public void Release(string key)
    {
        if (!redis.HasKey(key)) return;

        redis.DeleteKey(key);
    }
}