#region

using StackExchange.Redis;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// </summary>
public class RedisCache : IBaseCache
{
    /// <summary>
    /// </summary>
    private readonly IDatabase dataBase;

    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="db"></param>
    public RedisCache(string configuration, int db = default)
    {
        var connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
        dataBase = connectionMultiplexer.GetDatabase(db);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool DeleteKey(string key)
    {
        return dataBase.KeyDelete(key);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public T GetHash<T>(string key, string field)
    {
        if (!HasHash(key, field)) return default!;

        var json = dataBase.HashGet(key, field);

        return string.IsNullOrWhiteSpace(json) ? default! : json.ToString().DeJson<T>();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetList<T>(string key, int index)
    {
        if (!HasKey(key)) return default!;
        var json = dataBase.ListGetByIndex(key, index);

        return !string.IsNullOrWhiteSpace(json) ? json.ToString().DeJson<T>() : default!;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<T> GetList<T>(string key, int start, int end)
    {
        var list = new List<T>();

        if (!HasKey(key)) return list;

        foreach (var value in dataBase.ListRange(key, start, end).ToList())
            list.Add(value.ToString().DeJson<T>());

        return list;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<T> GetSortedSet<T>(string key, long start = 0, long end = int.MaxValue)
    {
        return GetSortedSet<T>(key, start, end, Order.Ascending);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetString<T>(string key)
    {
        if (!HasKey(key)) return default!;

        var json = dataBase.StringGet(key);

        return !string.IsNullOrWhiteSpace(json) ? json.ToString().DeJson<T>() : default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="Field"></param>
    /// <returns></returns>
    public bool HasHash(string key, string Field)
    {
        return dataBase.HashExists(key, Field);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool HasKey(string key)
    {
        return dataBase.KeyExists(key);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool LeftRemoveList(string key)
    {
        return string.IsNullOrWhiteSpace(dataBase.ListLeftPop(key));
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool LeftSetList<T>(string key, T value)
    {
        return dataBase.ListLeftPush(key, value?.ToJson()) > 0;
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<string> NextIdentity(NextIdentifyArg args)
    {
        var now = DateTime.Now.Date;

        var key = GetIdAsyncentityKey(args, now);
        var value = GetIdAsyncentifyValue(args, now);
        var time = GetIdAsyncentifyTimespan(args, now);

        if (HasKey(key))
        {
            value = GetString<long>(key);
            value += args.Count;
        }

        SetString(key, value, time);

        var result = $"{args.Prefix}{value}{args.Suffix}";

        return await Task.FromResult(result);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public bool RemoveHash(string key, string field)
    {
        return dataBase.HashDelete(key, field);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool RemoveList<T>(string key, T value)
    {
        return dataBase.ListRemove(key, value?.ToJson()) > 0;
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="newKey"></param>
    /// <returns></returns>
    public bool ReNameKey(string key, string newKey)
    {
        return dataBase.KeyRename(key, newKey);
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool RightRemoveList(string key)
    {
        return string.IsNullOrWhiteSpace(dataBase.ListRightPop(key));
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool RightSetList<T>(string key, T value)
    {
        return dataBase.ListRightPush(key, value?.ToJson()) > 0;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SetHash<T>(string key, string field, T value)
    {
        return dataBase.HashSet(key, field, value?.ToJson());
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    public bool SetKeyExpire(string key, TimeSpan expireTime)
    {
        return dataBase.KeyExpire(key, expireTime);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="score"></param>
    public void SetSortedSet<T>(string key, T value, double score)
    {
        dataBase.SortedSetAdd(key, value?.ToJson(), score);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    public bool SetString<T>(string key, T value, TimeSpan? expireTime = null)
    {
        return dataBase.StringSet(key, value?.ToJson(), expireTime);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public List<T> GetSortedSet<T>(string key, long start = 0, long end = int.MaxValue,
        Order order = Order.Descending)
    {
        var list = new List<T>();

        if (HasKey(key))
            foreach (var value in dataBase.SortedSetRangeByRank(key, start, end, order))
                list.Add(value.ToString().DeJson<T>());

        return list;
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    private static TimeSpan GetIdAsyncentifyTimespan(NextIdentifyArg args, DateTime now)
    {
        return args.Type switch
        {
            IdentifyEnum.Year => now.ToYearEnd() - now,
            IdentifyEnum.Month => now.ToMonthEnd() - now,
            IdentifyEnum.Day => now.ToDayEnd() - now,
            _ => now.ToDayEnd() - now
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    private static long GetIdAsyncentifyValue(NextIdentifyArg args, DateTime now)
    {
        return args.Type switch
        {
            IdentifyEnum.Year => long.Parse($"{now:yyyy}0000000000"),
            IdentifyEnum.Month => long.Parse($"{now:yyyyMM}00000000"),
            IdentifyEnum.Day => long.Parse($"{now:yyyyMMdd}000000"),
            _ => long.Parse($"{now:yyyyMMdd}000000")
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    private static string GetIdAsyncentityKey(NextIdentifyArg args, DateTime now)
    {
        return args.Type switch
        {
            IdentifyEnum.Year => $"{args.CompanyId}{args.Name}{now:yyyy}",
            IdentifyEnum.Month => $"{args.CompanyId}{args.Name}{now:yyyyMM}",
            IdentifyEnum.Day => $"{args.CompanyId}{args.Name}{now:yyyyMMdd}",
            _ => $"{args.CompanyId}{args.Name}{now:yyyyMMdd}"
        };
    }
}