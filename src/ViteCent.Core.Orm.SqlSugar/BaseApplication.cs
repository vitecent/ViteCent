#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseApplication<T> : IBaseApplication<T> where T : IBaseEntity, new()
{
    /// <summary>
    /// </summary>
    protected BaseApplication()
    {
    }

    /// <summary>
    /// </summary>
    public abstract IBaseDomain<T> Domain { get; }

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<BaseResult> AddAsync(T entity)
    {
        return await Domain.AddAsync(entity);
    }

    /// <summary>
    /// </summary>
    /// <param name="entitys"></param>
    /// <returns></returns>
    public virtual async Task<BaseResult> AddAsync(List<T> entitys)
    {
        return await Domain.AddAsync(entitys);
    }

    /// <summary>
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public async Task<BaseResult> DeleteAsync(Expression<Func<T, bool>> where)
    {
        return await Domain.DeleteAsync(where);
    }

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<BaseResult> DeleteAsync(T entity)
    {
        return await Domain.DeleteAsync(entity);
    }

    /// <summary>
    /// </summary>
    /// <param name="entitys"></param>
    /// <returns></returns>
    public async Task<BaseResult> DeleteAsync(List<T> entitys)
    {
        return await Domain.DeleteAsync(entitys);
    }

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<BaseResult> EditAsync(T entity)
    {
        return await Domain.EditAsync(entity);
    }

    /// <summary>
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        return await Domain.GetAsync(where);
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual async Task<List<T>> PageAsync(SearchArgs args)
    {
        return await Domain.PageAsync(args);
    }
}