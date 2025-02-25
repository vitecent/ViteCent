#region

using StackExchange.Redis;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
///     Class RedisCache. Implements the <see cref="ViteCent.Core.Cache.IBaseCache" />
/// </summary>
/// <seealso cref="ViteCent.Core.Cache.IBaseCache" />
public class RedisCache : IBaseCache
{
    /// <summary>
    ///     The DataBase
    /// </summary>
    private readonly IDatabase DataBase;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedisCache" /> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="db">The DataBase.</param>
    public RedisCache(string configuration, int db = default)
    {
        var connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
        DataBase = connectionMultiplexer.GetDatabase(db);
    }

    /// <summary>
    ///     Deletes the key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>result</returns>
    public bool DeleteKey(string key)
    {
        return DataBase.KeyDelete(key);
    }

    /// <summary>
    ///     Gets the hash.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="field">The field.</param>
    /// <returns>T.</returns>
    public T GetHash<T>(string key, string field)
    {
        if (HasHash(key, field))
        {
            var json = DataBase.HashGet(key, field);

            if (!string.IsNullOrWhiteSpace(json)) return json.ToString().DeJson<T>();
        }

        return default!;
    }

    /// <summary>
    ///     Gets the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="index">The index.</param>
    /// <returns>T.</returns>
    public T GetList<T>(string key, int index)
    {
        if (HasKey(key))
        {
            var json = DataBase.ListGetByIndex(key, index);

            if (!string.IsNullOrWhiteSpace(json)) return json.ToString().DeJson<T>();
        }

        return default!;
    }

    /// <summary>
    ///     Gets the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="start">The start.</param>
    /// <param name="end">The end.</param>
    /// <returns>List&lt;T&gt;.</returns>
    public List<T> GetList<T>(string key, int start, int end)
    {
        var list = new List<T>();

        if (HasKey(key))
            foreach (var value in DataBase.ListRange(key, start, end).ToList())
                list.Add(value.ToString().DeJson<T>());

        return list;
    }

    /// <summary>
    ///     Gets the sorted set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="strart">The strart.</param>
    /// <param name="stop">The stop.</param>
    /// <returns>List&lt;T&gt;.</returns>
    public List<T> GetSortedSet<T>(string key, long strart = default, long stop = int.MaxValue)
    {
        return GetSortedSet<T>(key, strart, stop, Order.Ascending);
    }

    /// <summary>
    ///     Gets the sorted set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="strart">The strart.</param>
    /// <param name="stop">The stop.</param>
    /// <param name="order">The order.</param>
    /// <returns>List&lt;T&gt;.</returns>
    public List<T> GetSortedSet<T>(string key, long strart = default, long stop = int.MaxValue,
        Order order = Order.Descending)
    {
        var list = new List<T>();

        if (HasKey(key))
            foreach (var value in DataBase.SortedSetRangeByRank(key, strart, stop, order))
                list.Add(value.ToString().DeJson<T>());

        return list;
    }

    /// <summary>
    ///     Gets the string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <returns>T.</returns>
    public T GetString<T>(string key)
    {
        if (HasKey(key))
        {
            var json = DataBase.StringGet(key);

            if (!string.IsNullOrWhiteSpace(json)) return json.ToString().DeJson<T>();
        }

        return default!;
    }

    /// <summary>
    ///     Determines whether the specified key has hash.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="Field">The field.</param>
    /// <returns><c>true</c> if the specified key has hash; otherwise, <c>false</c>.</returns>
    public bool HasHash(string key, string Field)
    {
        return DataBase.HashExists(key, Field);
    }

    /// <summary>
    ///     Determines whether the specified key has key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns><c>true</c> if the specified key has key; otherwise, <c>false</c>.</returns>
    public bool HasKey(string key)
    {
        return DataBase.KeyExists(key);
    }

    /// <summary>
    ///     Lefts the remove list.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>result</returns>
    public bool LeftRemoveList(string key)
    {
        return string.IsNullOrWhiteSpace(DataBase.ListLeftPop(key));
    }

    /// <summary>
    ///     Lefts the set list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    public bool LeftSetList<T>(string key, T value)
    {
        return DataBase.ListLeftPush(key, value?.ToJson()) > 0;
    }

    /// <summary>
    ///     Removes the hash.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="field">The field.</param>
    /// <returns>result</returns>
    public bool RemoveHash(string key, string field)
    {
        return DataBase.HashDelete(key, field);
    }

    /// <summary>
    ///     Removes the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    public bool RemoveList<T>(string key, T value)
    {
        return DataBase.ListRemove(key, value?.ToJson()) > 0;
    }

    /// <summary>
    ///     Res the name key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="newKey">The new key.</param>
    /// <returns>result</returns>
    public bool ReNameKey(string key, string newKey)
    {
        return DataBase.KeyRename(key, newKey);
    }

    /// <summary>
    ///     Rights the remove list.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>result</returns>
    public bool RightRemoveList(string key)
    {
        return string.IsNullOrWhiteSpace(DataBase.ListRightPop(key));
    }

    /// <summary>
    ///     Rights the set list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    public bool RightSetList<T>(string key, T value)
    {
        return DataBase.ListRightPush(key, value?.ToJson()) > 0;
    }

    /// <summary>
    ///     Sets the hash.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    public bool SetHash<T>(string key, string field, T value)
    {
        return DataBase.HashSet(key, field, value?.ToJson());
    }

    /// <summary>
    ///     Sets the key expire.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="expireTime">The expire time.</param>
    /// <returns>result</returns>
    public bool SetKeyExpire(string key, TimeSpan expireTime)
    {
        return DataBase.KeyExpire(key, expireTime);
    }

    /// <summary>
    ///     Sets the sorted set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="score">The score.</param>
    public void SetSortedSet<T>(string key, T value, double score)
    {
        DataBase.SortedSetAdd(key, value?.ToJson(), score);
    }

    /// <summary>
    ///     Sets the string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="expireTime">The expire time.</param>
    /// <returns>result</returns>
    public bool SetString<T>(string key, T value, TimeSpan? expireTime = null)
    {
        return DataBase.StringSet(key, value?.ToJson(), expireTime);
    }
}