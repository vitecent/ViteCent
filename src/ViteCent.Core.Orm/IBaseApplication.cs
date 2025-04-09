#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseApplication<T> where T : IBaseEntity, new()
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<BaseResult> AddAsync(T entity);

    /// <summary>
    /// </summary>
    /// <param name="entitys"></param>
    /// <returns></returns>
    Task<BaseResult> AddAsync(List<T> entitys);

    /// <summary>
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    Task<BaseResult> DeleteAsync(Expression<Func<T, bool>> where);

    /// <summary>
    /// </summary>
    /// <param name="entitys"></param>
    /// <returns></returns>
    Task<BaseResult> DeleteAsync(List<T> entitys);

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<BaseResult> DeleteAsync(T entity);

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<BaseResult> EditAsync(T entity);

    /// <summary>
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    Task<T> GetAsync(Expression<Func<T, bool>> where);

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Task<List<T>> PageAsync(SearchArgs args);
}