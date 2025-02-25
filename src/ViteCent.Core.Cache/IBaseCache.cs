namespace ViteCent.Core.Cache;

/// <summary>
///     Interface ICache
/// </summary>
public interface IBaseCache
{
    /// <summary>
    ///     Deletes the key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>result</returns>
    bool DeleteKey(string key);

    /// <summary>
    ///     Gets the hash.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="field">The field.</param>
    /// <returns>T.</returns>
    T GetHash<T>(string key, string field);

    /// <summary>
    ///     Gets the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="index">The index.</param>
    /// <returns>T.</returns>
    T GetList<T>(string key, int index);

    /// <summary>
    ///     Gets the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="start">The start.</param>
    /// <param name="end">The end.</param>
    /// <returns>List&lt;T&gt;.</returns>
    List<T> GetList<T>(string key, int start, int end);

    /// <summary>
    ///     Gets the sorted set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="start">The start.</param>
    /// <param name="stop">The stop.</param>
    /// <returns>List&lt;T&gt;.</returns>
    List<T> GetSortedSet<T>(string key, long start = 0, long stop = int.MaxValue);

    /// <summary>
    ///     Gets the string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <returns>T.</returns>
    T GetString<T>(string key);

    /// <summary>
    ///     Determines whether the specified key has hash.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="Field">The field.</param>
    /// <returns><c>true</c> if the specified key has hash; otherwise, <c>false</c>.</returns>
    bool HasHash(string key, string Field);

    /// <summary>
    ///     Determines whether the specified key has key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns><c>true</c> if the specified key has key; otherwise, <c>false</c>.</returns>
    bool HasKey(string key);

    /// <summary>
    ///     Lefts the remove list.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>result</returns>
    bool LeftRemoveList(string key);

    /// <summary>
    ///     Lefts the set list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    bool LeftSetList<T>(string key, T value);

    /// <summary>
    ///     Removes the hash.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="field">The field.</param>
    /// <returns>result</returns>
    bool RemoveHash(string key, string field);

    /// <summary>
    ///     Removes the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    bool RemoveList<T>(string key, T value);

    /// <summary>
    ///     Res the name key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="newKey">The new key.</param>
    /// <returns>result</returns>
    bool ReNameKey(string key, string newKey);

    /// <summary>
    ///     Rights the remove list.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>result</returns>
    bool RightRemoveList(string key);

    /// <summary>
    ///     Rights the set list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    bool RightSetList<T>(string key, T value);

    /// <summary>
    ///     Sets the hash.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>result</returns>
    bool SetHash<T>(string key, string field, T value);

    /// <summary>
    ///     Sets the key expire.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="expireTime">The expire time.</param>
    /// <returns>result</returns>
    bool SetKeyExpire(string key, TimeSpan expireTime);

    /// <summary>
    ///     Sets the sorted set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="score">The score.</param>
    void SetSortedSet<T>(string key, T value, double score);

    /// <summary>
    ///     Sets the string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="expireTime">The expire time.</param>
    /// <returns>result</returns>
    bool SetString<T>(string key, T value, TimeSpan? expireTime = null);
}