#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
///     Interface IBaseApplication
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseApplication<T> where T : IBaseEntity, new()
{
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