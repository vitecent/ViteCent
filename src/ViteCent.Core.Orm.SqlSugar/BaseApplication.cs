#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// 应用层基类，提供基础的CRUD操作功能
/// </summary>
/// <typeparam name="T">实体类型，必须实现IBaseEntity接口且可实例化</typeparam>
public abstract class BaseApplication<T> : IBaseApplication<T> where T : IBaseEntity, new()
{
    /// <summary>
    /// 初始化应用层基类实例
    /// </summary>
    protected BaseApplication()
    {
    }

    /// <summary>
    /// 获取或设置领域层实例
    /// </summary>
    public abstract IBaseDomain<T> Domain { get; }

    /// <summary>
    /// 异步添加单个实体
    /// </summary>
    /// <param name="entity">要添加的实体对象</param>
    /// <returns>返回操作结果</returns>
    public virtual async Task<BaseResult> AddAsync(T entity)
    {
        return await Domain.AddAsync(entity);
    }

    /// <summary>
    /// 异步批量添加实体
    /// </summary>
    /// <param name="entitys">要添加的实体对象列表</param>
    /// <returns>返回操作结果</returns>
    public virtual async Task<BaseResult> AddAsync(List<T> entitys)
    {
        return await Domain.AddAsync(entitys);
    }

    /// <summary>
    /// 根据条件异步删除实体
    /// </summary>
    /// <param name="where">删除条件表达式</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> DeleteAsync(Expression<Func<T, bool>> where)
    {
        return await Domain.DeleteAsync(where);
    }

    /// <summary>
    /// 异步删除单个实体
    /// </summary>
    /// <param name="entity">要删除的实体对象</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> DeleteAsync(T entity)
    {
        return await Domain.DeleteAsync(entity);
    }

    /// <summary>
    /// 异步批量删除实体
    /// </summary>
    /// <param name="entitys">要删除的实体对象列表</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> DeleteAsync(List<T> entitys)
    {
        return await Domain.DeleteAsync(entitys);
    }

    /// <summary>
    /// 异步更新实体
    /// </summary>
    /// <param name="entity">要更新的实体对象</param>
    /// <returns>返回操作结果</returns>
    public virtual async Task<BaseResult> EditAsync(T entity)
    {
        return await Domain.EditAsync(entity);
    }

    /// <summary>
    /// 根据条件异步获取单个实体
    /// </summary>
    /// <param name="where">查询条件表达式</param>
    /// <returns>返回符合条件的实体对象</returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        return await Domain.GetAsync(where);
    }

    /// <summary>
    /// 异步分页查询实体列表
    /// </summary>
    /// <param name="args">分页查询参数</param>
    /// <returns>返回分页后的实体对象列表</returns>
    public virtual async Task<List<T>> PageAsync(SearchArgs args)
    {
        return await Domain.PageAsync(args);
    }
}