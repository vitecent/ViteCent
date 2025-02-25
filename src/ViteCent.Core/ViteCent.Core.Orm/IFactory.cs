#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
///     Interface IFactory
/// </summary>
public interface IFactory
{
    /// <summary>
    ///     Commits the asynchronous.
    /// </summary>
    /// <returns>Task&lt;BaseResult&gt;.</returns>
    Task<BaseResult> CommitAsync();

    /// <summary>
    ///     Deletes the specified where.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="where">The where.</param>
    void Delete<T>(Expression<Func<T, bool>> where) where T : class, new();

    /// <summary>
    ///     Deletes the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    void Delete<T>(List<T> entitys) where T : class, new();

    /// <summary>
    ///     Deletes the specified SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql">The SQL.</param>
    /// <param name="parameters">The parameters.</param>
    void Delete<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    ///     Deletes the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    void Delete<T>(T entity) where T : class, new();

    /// <summary>
    ///     Inserts the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    void Insert<T>(List<T> entitys) where T : class, new();

    /// <summary>
    ///     Inserts the specified SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql">The SQL.</param>
    /// <param name="parameters">The parameters.</param>
    void Insert<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    ///     Inserts the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    void Insert<T>(T entity) where T : class, new();

    /// <summary>
    ///     Pages the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
    Task<List<T>> PageAsync<T>(SearchArgs args) where T : class, new();

    /// <summary>
    ///     Updates the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    void Update<T>(List<T> entitys) where T : class, new();

    /// <summary>
    ///     Updates the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    /// <param name="columns">The columns.</param>
    void Update<T>(List<T> entitys, Expression<Func<T, object>> columns) where T : class, new();

    /// <summary>
    ///     Updates the specified SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql">The SQL.</param>
    /// <param name="parameters">The parameters.</param>
    void Update<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    ///     Updates the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    void Update<T>(T entity) where T : class, new();

    /// <summary>
    ///     Updates the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    /// <param name="columns">The columns.</param>
    void Update<T>(T entity, Expression<Func<T, object>> columns) where T : class, new();
}