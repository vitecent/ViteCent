#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
///     Interface IBaseDal
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseDomain<T> where T : IBaseEntity, new()
{
    /// <summary>
    ///     Gets the DataBase.
    /// </summary>
    /// <value>The DataBase.</value>
    string DataBaseName { get; }

    /// <summary>
    ///     Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    /// <returns>Task&lt;BaseResult&gt;.</returns>
    Task<BaseResult> AddAsync(T entity);

    /// <summary>
    ///     Edits the asynchronous.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    /// <returns>Task&lt;BaseResult&gt;.</returns>
    Task<BaseResult> EditAsync(T entity);

    /// <summary>
    ///     Gets the asynchronous.
    /// </summary>
    /// <param name="where">The where.</param>
    /// <returns>Task&lt;T&gt;.</returns>
    Task<T> GetAsync(Expression<Func<T, bool>> where);

    /// <summary>
    ///     Pages the asynchronous.
    /// </summary>
    /// <param name="args"></param>
    /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
    Task<List<T>> PageAsync(SearchArgs args);
}