#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
///     Class BaseDal. Implements the <see cref="ViteCent.Core.Orm.IBaseDomain{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="ViteCent.Core.Orm.IBaseDomain{T}" />
public abstract class BaseDomain<T> : IBaseDomain<T> where T : BaseEntity, new()
{
    /// <summary>
    ///     The client
    /// </summary>
    public readonly SqlSugarFactory Client;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseDomain{T}" /> class.
    /// </summary>
    protected BaseDomain()
    {
        Client = new SqlSugarFactory(DataBaseName);
    }

    /// <summary>
    ///     Gets the DataBase.
    /// </summary>
    /// <value>The DataBase.</value>
    public abstract string DataBaseName { get; }

    /// <summary>
    ///     Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    /// <returns>A Task&lt;BaseResult&gt; representing the asynchronous operation.</returns>
    public virtual async Task<BaseResult> AddAsync(T entity)
    {
        Client.Insert(entity);
        return await Client.CommitAsync();
    }

    /// <summary>
    ///     Edits the asynchronous.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    /// <returns>A Task&lt;BaseResult&gt; representing the asynchronous operation.</returns>
    public virtual async Task<BaseResult> EditAsync(T entity)
    {
        Client.Update(entity);
        return await Client.CommitAsync();
    }

    /// <summary>
    ///     Gets the asynchronous.
    /// </summary>
    /// <param name="where">The where.</param>
    /// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        var Entity = await Client.Query<T>().Where(where).FirstAsync();
        return Entity ?? default!;
    }

    /// <summary>
    ///     Page as an asynchronous operation.
    /// </summary>
    /// <param name="args"></param>
    /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
    public virtual async Task<List<T>> PageAsync(SearchArgs args)
    {
        var list = await Client.PageAsync<T>(args);
        return list ?? default!;
    }
}