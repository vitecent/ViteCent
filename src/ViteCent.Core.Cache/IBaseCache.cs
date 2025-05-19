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
    /// 检查哈希表中是否存在指定字段
    /// </summary>
    /// <param name="key">哈希表的键</param>
    /// <param name="Field">要检查的字段名</param>
    /// <returns>存在返回true，否则返回false</returns>
    bool HasHash(string key, string Field);

    /// <summary>
    /// 检查缓存中是否存在指定的键
    /// </summary>
    /// <param name="key">要检查的缓存键</param>
    /// <returns>存在返回true，否则返回false</returns>
    bool HasKey(string key);

    /// <summary>
    /// 从列表左侧移除一个元素
    /// </summary>
    /// <param name="key">列表的键</param>
    /// <returns>移除成功返回true，否则返回false</returns>
    bool LeftRemoveList(string key);

    /// <summary>
    /// 从列表左侧插入一个元素
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="key">列表的键</param>
    /// <param name="value">要插入的值</param>
    /// <returns>插入成功返回true，否则返回false</returns>
    bool LeftSetList<T>(string key, T value);

    /// <summary>
    /// 获取下一个标识符
    /// </summary>
    /// <param name="args">标识符生成参数</param>
    /// <returns>返回生成的标识符字符串</returns>
    Task<string> NextIdentity(NextIdentifyArg args);

    /// <summary>
    /// 从哈希表中移除指定字段
    /// </summary>
    /// <param name="key">哈希表的键</param>
    /// <param name="field">要移除的字段名</param>
    /// <returns>移除成功返回true，否则返回false</returns>
    bool RemoveHash(string key, string field);

    /// <summary>
    /// 从列表中移除指定的值
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="key">列表的键</param>
    /// <param name="value">要移除的值</param>
    /// <returns>移除成功返回true，否则返回false</returns>
    bool RemoveList<T>(string key, T value);

    /// <summary>
    /// 重命名缓存键
    /// </summary>
    /// <param name="key">原缓存键</param>
    /// <param name="newKey">新的缓存键</param>
    /// <returns>重命名成功返回true，否则返回false</returns>
    bool ReNameKey(string key, string newKey);

    /// <summary>
    /// 从列表右侧移除一个元素
    /// </summary>
    /// <param name="key">列表的键</param>
    /// <returns>移除成功返回true，否则返回false</returns>
    bool RightRemoveList(string key);

    /// <summary>
    /// 从列表右侧插入一个元素
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="key">列表的键</param>
    /// <param name="value">要插入的值</param>
    /// <returns>插入成功返回true，否则返回false</returns>
    bool RightSetList<T>(string key, T value);

    /// <summary>
    /// 设置哈希表中字段的值
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="key">哈希表的键</param>
    /// <param name="field">字段名</param>
    /// <param name="value">要设置的值</param>
    /// <returns>设置成功返回true，否则返回false</returns>
    bool SetHash<T>(string key, string field, T value);

    /// <summary>
    /// 设置缓存键的过期时间
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="expireTime">过期时间间隔</param>
    /// <returns>设置成功返回true，否则返回false</returns>
    bool SetKeyExpire(string key, TimeSpan expireTime);

    /// <summary>
    /// 向有序集合中添加一个值和其对应的分数
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="key">有序集合的键</param>
    /// <param name="value">要添加的值</param>
    /// <param name="score">值对应的分数</param>
    void SetSortedSet<T>(string key, T value, double score);

    /// <summary>
    /// 设置字符串类型的缓存值
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">要设置的值</param>
    /// <param name="expireTime">可选的过期时间</param>
    /// <returns>设置成功返回true，否则返回false</returns>
    bool SetString<T>(string key, T value, TimeSpan? expireTime = null);
}