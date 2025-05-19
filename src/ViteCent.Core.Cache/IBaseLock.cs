namespace ViteCent.Core.Cache;

/// <summary>
/// 分布式锁接口，提供基本的分布式锁操作功能
/// </summary>
public interface IBaseLock
{
    /// <summary>
    /// 尝试获取指定键的分布式锁
    /// </summary>
    /// <param name="key">锁的唯一标识键</param>
    /// <param name="time">锁的持续时间</param>
    /// <returns>获取锁成功返回true，否则返回false</returns>
    bool Lock(string key, TimeSpan time);

    /// <summary>
    /// 释放指定键的分布式锁
    /// </summary>
    /// <param name="key">要释放的锁的唯一标识键</param>
    void Release(string key);
}