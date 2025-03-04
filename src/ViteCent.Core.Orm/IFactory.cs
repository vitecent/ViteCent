#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public interface IFactory
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task<BaseResult> CommitAsync();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="where"></param>
    void Delete<T>(Expression<Func<T, bool>> where) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    void Delete<T>(List<T> entitys) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    void Delete<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Delete<T>(T entity) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    void Insert<T>(List<T> entitys) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    void Insert<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Insert<T>(T entity) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns></returns>
    Task<List<T>> PageAsync<T>(SearchArgs args) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    void Update<T>(List<T> entitys) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    /// <param name="columns"></param>
    void Update<T>(List<T> entitys, Expression<Func<T, object>> columns) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    void Update<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Update<T>(T entity) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="columns"></param>
    void Update<T>(T entity, Expression<Func<T, object>> columns) where T : class, new();
}