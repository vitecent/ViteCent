#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
///     Class BaseApplication. Implements the <see cref="ViteCent.Core.Orm.IBaseApplication{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="ViteCent.Core.Orm.IBaseApplication{T}" />
public abstract class BaseApplication<T> : IBaseApplication<T> where T : IBaseEntity, new()
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseApplication{T}" /> class.
    /// </summary>
    protected BaseApplication()
    {
    }

    /// <summary>
    ///     Gets the domain.
    /// </summary>
    /// <value>The domain.</value>
    public abstract IBaseDomain<T> Domain { get; }

    /// <summary>
    ///     Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    /// <returns>A Task&lt;BaseResult&gt; representing the asynchronous operation.</returns>
    public virtual async Task<BaseResult> AddAsync(T entity)
    {
        return await Domain.AddAsync(entity);
    }

    /// <summary>
    ///     Edits the asynchronous.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    /// <returns>A Task&lt;BaseResult&gt; representing the asynchronous operation.</returns>
    public virtual async Task<BaseResult> EditAsync(T entity)
    {
        return await Domain.EditAsync(entity);
    }

    /// <summary>
    ///     Gets the asynchronous.
    /// </summary>
    /// <param name="where">The where.</param>
    /// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        return await Domain.GetAsync(where);
    }

    /// <summary>
    ///     Page as an asynchronous operation.
    /// </summary>
    /// <param name="args"></param>
    /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
    public virtual async Task<List<T>> PageAsync(SearchArgs args)
    {
        return await Domain.PageAsync(args);
    }
}