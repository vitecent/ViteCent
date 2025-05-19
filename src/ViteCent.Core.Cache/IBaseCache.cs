using ViteCent.Core.Data;

namespace ViteCent.Core.Cache;

/// <summary>
/// 定义缓存操作的基本接口，提供对缓存数据的基本操作方法
/// </summary>
public interface IBaseCache
{
    /// <summary>
    /// 删除指定键的缓存数据
    /// </summary>
    /// <param name="key">要删除的缓存键</param>
    /// <returns>删除成功返回true，否则返回false</returns>
    bool DeleteKey(string key);

    /// <summary>
    /// 获取哈希表中指定字段的值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="key">哈希表的键</param>
    /// <param name="field">字段名</param>
    /// <returns>返回指定类型的值，如果不存在则返回默认值</returns>
    T GetHash<T>(string key, string field);

    /// <summary>
    /// 获取列表中指定索引位置的值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="key">列表的键</param>
    /// <param name="index">要获取的索引位置</param>
    /// <returns>返回指定类型的值，如果不存在则返回默认值</returns>
    T GetList<T>(string key, int index);

    /// <summary>
    /// 获取列表中指定范围的值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="key">列表的键</param>
    /// <param name="start">起始索引</param>
    /// <param name="end">结束索引</param>
    /// <returns>返回指定范围内的值列表</returns>
    List<T> GetList<T>(string key, int start, int end);

    /// <summary>
    /// 获取有序集合中指定分数范围的值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="key">有序集合的键</param>
    /// <param name="start">起始索引</param>
    /// <param name="end">结束索引</param>
    /// <returns>返回指定范围内的值列表</returns>
    List<T> GetSortedSet<T>(string key, long start = 0, long end = int.MaxValue);

    /// <summary>
    /// 获取字符串类型的缓存值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>返回指定类型的值，如果不存在则返回默认值</returns>
    T GetString<T>(string key);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="Field"></param>
    /// <returns></returns>
    bool HasHash(string key, string Field);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool HasKey(string key);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool LeftRemoveList(string key);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool LeftSetList<T>(string key, T value);

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Task<string> NextIdentity(NextIdentifyArg args);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    bool RemoveHash(string key, string field);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool RemoveList<T>(string key, T value);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="newKey"></param>
    /// <returns></returns>
    bool ReNameKey(string key, string newKey);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool RightRemoveList(string key);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool RightSetList<T>(string key, T value);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool SetHash<T>(string key, string field, T value);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    bool SetKeyExpire(string key, TimeSpan expireTime);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="score"></param>
    void SetSortedSet<T>(string key, T value, double score);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    bool SetString<T>(string key, T value, TimeSpan? expireTime = null);
}