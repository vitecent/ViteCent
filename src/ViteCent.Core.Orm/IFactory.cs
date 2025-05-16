#region

using System.Linq.Expressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// 数据访问工厂接口，定义了数据库操作的基本契约
/// </summary>
public interface IFactory
{
    /// <summary>
    /// 提交事务，将所有挂起的更改保存到数据库
    /// </summary>
    /// <returns>返回操作结果，包含成功状态和错误信息</returns>
    Task<BaseResult> CommitAsync();

    /// <summary>
    /// 根据条件删除数据
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="where">删除条件表达式</param>
    void Delete<T>(Expression<Func<T, bool>> where) where T : class, new();

    /// <summary>
    /// 批量删除实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entitys">要删除的实体列表</param>
    void Delete<T>(List<T> entitys) where T : class, new();

    /// <summary>
    /// 使用SQL语句删除数据
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="sql">SQL删除语句</param>
    /// <param name="parameters">SQL参数</param>
    void Delete<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">要删除的实体</param>
    void Delete<T>(T entity) where T : class, new();

    /// <summary>
    /// 批量插入实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entitys">要插入的实体列表</param>
    void Insert<T>(List<T> entitys) where T : class, new();

    /// <summary>
    /// 使用SQL语句插入数据
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="sql">SQL插入语句</param>
    /// <param name="parameters">SQL参数</param>
    void Insert<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    /// 插入单个实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">要插入的实体</param>
    void Insert<T>(T entity) where T : class, new();

    /// <summary>
    /// 执行分页查询
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="args">查询参数，包含页码、每页大小等信息</param>
    /// <returns>返回分页后的实体列表</returns>
    Task<List<T>> PageAsync<T>(SearchArgs args) where T : class, new();

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entitys">要更新的实体列表</param>
    void Update<T>(List<T> entitys) where T : class, new();

    /// <summary>
    /// 批量更新实体的指定列
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entitys">要更新的实体列表</param>
    /// <param name="columns">要更新的列的表达式</param>
    void Update<T>(List<T> entitys, Expression<Func<T, object>> columns) where T : class, new();

    /// <summary>
    /// 使用SQL语句更新数据
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="sql">SQL更新语句</param>
    /// <param name="parameters">SQL参数</param>
    void Update<T>(string sql, object parameters = default!) where T : class, new();

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">要更新的实体</param>
    void Update<T>(T entity) where T : class, new();

    /// <summary>
    /// 更新单个实体的指定列
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">要更新的实体</param>
    /// <param name="columns">要更新的列的表达式</param>
    void Update<T>(T entity, Expression<Func<T, object>> columns) where T : class, new();
}