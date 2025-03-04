using ViteCent.Core.Data;

namespace ViteCent.Core.Cache;

/// <summary>
/// </summary>
public interface IBaseCache
{
    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool DeleteKey(string key);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    T GetHash<T>(string key, string field);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    T GetList<T>(string key, int index);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    List<T> GetList<T>(string key, int start, int end);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    List<T> GetSortedSet<T>(string key, long start = 0, long end = int.MaxValue);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
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