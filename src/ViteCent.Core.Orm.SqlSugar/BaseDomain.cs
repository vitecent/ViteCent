#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseDomain<T> : IBaseDomain<T> where T : BaseEntity, new()
{
    /// <summary>
    /// </summary>
    public readonly SqlSugarFactory Client;

    /// <summary>
    /// </summary>
    protected BaseDomain()
    {
        Client = new SqlSugarFactory(DataBaseName);
    }

    /// <summary>
    /// </summary>
    /// <value>The DataBase.</value>
    public abstract string DataBaseName { get; }

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<BaseResult> AddAsync(T entity)
    {
        Client.Insert(entity);
        return await Client.CommitAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public async Task<BaseResult> DeleteAsync(Expression<Func<T, bool>> where)
    {
        Client.Delete(where);
        return await Client.CommitAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<BaseResult> EditAsync(T entity)
    {
        Client.Update(entity);
        return await Client.CommitAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        var entity = await Client.Query<T>().Where(where).FirstAsync();
        return entity ?? default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual async Task<List<T>> PageAsync(SearchArgs args)
    {
        var list = await Client.PageAsync<T>(args);
        return list ?? default!;
    }
}