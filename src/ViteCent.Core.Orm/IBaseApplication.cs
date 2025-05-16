#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// 应用层基础接口，定义了对实体的基本操作契约
/// </summary>
/// <typeparam name="T">实体类型，必须实现IBaseEntity接口且可实例化</typeparam>
public interface IBaseApplication<T> where T : IBaseEntity, new()
{
    /// <summary>
    /// 异步添加单个实体
    /// </summary>
    /// <param name="entity">待添加的实体对象</param>
    /// <returns>返回操作结果，包含成功状态和相关信息</returns>
    Task<BaseResult> AddAsync(T entity);

    /// <summary>
    /// 异步批量添加实体
    /// </summary>
    /// <param name="entitys">待添加的实体对象列表</param>
    /// <returns>返回操作结果，包含成功状态和相关信息</returns>
    Task<BaseResult> AddAsync(List<T> entitys);

    /// <summary>
    /// 根据条件异步删除实体
    /// </summary>
    /// <param name="where">删除条件表达式</param>
    /// <returns>返回操作结果，包含成功状态和相关信息</returns>
    Task<BaseResult> DeleteAsync(Expression<Func<T, bool>> where);

    /// <summary>
    /// 异步批量删除实体
    /// </summary>
    /// <param name="entitys">待删除的实体对象列表</param>
    /// <returns>返回操作结果，包含成功状态和相关信息</returns>
    Task<BaseResult> DeleteAsync(List<T> entitys);

    /// <summary>
    /// 异步删除单个实体
    /// </summary>
    /// <param name="entity">待删除的实体对象</param>
    /// <returns>返回操作结果，包含成功状态和相关信息</returns>
    Task<BaseResult> DeleteAsync(T entity);

    /// <summary>
    /// 异步更新实体
    /// </summary>
    /// <param name="entity">待更新的实体对象</param>
    /// <returns>返回操作结果，包含成功状态和相关信息</returns>
    Task<BaseResult> EditAsync(T entity);

    /// <summary>
    /// 根据条件异步获取单个实体
    /// </summary>
    /// <param name="where">查询条件表达式</param>
    /// <returns>返回符合条件的实体对象</returns>
    Task<T> GetAsync(Expression<Func<T, bool>> where);

    /// <summary>
    /// 异步分页查询实体列表
    /// </summary>
    /// <param name="args">分页查询参数</param>
    /// <returns>返回分页后的实体对象列表</returns>
    Task<List<T>> PageAsync(SearchArgs args);
}