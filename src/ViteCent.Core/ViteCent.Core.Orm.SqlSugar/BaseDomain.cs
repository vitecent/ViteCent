#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// 领域层基类，提供基础的CRUD操作功能
/// </summary>
/// <typeparam name="T">实体类型，必须继承自BaseEntity且可实例化</typeparam>
public abstract class BaseDomain<T> : IBaseDomain<T> where T : BaseEntity, new()
{
    /// <summary>
    /// SqlSugar数据库操作客户端实例
    /// </summary>
    public readonly SqlSugarFactory Client;

    /// <summary>
    /// 构造函数，初始化数据库连接
    /// </summary>
    protected BaseDomain()
    {
        Client = new SqlSugarFactory(Database);
    }

    /// <summary>
    /// 获取数据库名称
    /// </summary>
    public abstract string Database { get; }

    /// <summary>
    /// 添加单个实体
    /// </summary>
    /// <param name="entity">待添加的实体对象</param>
    /// <returns>返回操作结果</returns>
    public virtual async Task<BaseResult> AddAsync(T entity)
    {
        try
        {
            var flag = await Client.Insert(entity).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            return new BaseResult(500, e.Message);
        }

        return new BaseResult();
    }

    /// <summary>
    /// 批量添加实体
    /// </summary>
    /// <param name="entitys">待添加的实体对象列表</param>
    /// <returns>返回操作结果</returns>
    public virtual async Task<BaseResult> AddAsync(List<T> entitys)
    {
        try
        {
            var flag = await Client.Insert(entitys).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            return new BaseResult(500, e.Message);
        }

        return new BaseResult();
    }

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <param name="where">删除条件表达式</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> DeleteAsync(Expression<Func<T, bool>> where)
    {
        try
        {
            var flag = await Client.Delete<T>().Where(where).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            return new BaseResult(500, e.Message);
        }

        return new BaseResult();
    }

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity">待删除的实体对象</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> DeleteAsync(T entity)
    {
        try
        {
            var flag = await Client.Delete(entity).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            return new BaseResult(500, e.Message);
        }

        return new BaseResult();
    }

    /// <summary>
    /// 批量删除实体
    /// </summary>
    /// <param name="entitys">待删除的实体对象列表</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> DeleteAsync(List<T> entitys)
    {
        try
        {
            var flag = await Client.Delete(entitys).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            return new BaseResult(500, e.Message);
        }

        return new BaseResult();
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">待更新的实体对象</param>
    /// <returns>返回操作结果</returns>
    public virtual async Task<BaseResult> EditAsync(T entity)
    {
        try
        {
            var flag = await Client.Update(entity).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            return new BaseResult(500, e.Message);
        }

        return new BaseResult();
    }

    /// <summary>
    /// 根据条件查询单个实体
    /// </summary>
    /// <param name="where">查询条件表达式</param>
    /// <returns>返回符合条件的第一个实体，如果没有找到则返回默认值</returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        var entity = await Client.Query<T>().Where(where).FirstAsync();

        return entity ?? default!;
    }

    /// <summary>
    /// 分页查询实体列表
    /// </summary>
    /// <param name="args">分页查询参数</param>
    /// <returns>返回分页后的实体列表，如果没有数据则返回默认值</returns>
    public virtual async Task<List<T>> PageAsync(SearchArgs args)
    {
        var list = await Client.PageAsync<T>(args);

        return list ?? default!;
    }
}