namespace ViteCent.Core.Orm;

/// <summary>
/// 数据访问工厂接口，定义了数据库操作的基本契约
/// </summary>
public interface IFactory
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task BeginTranAsync();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task CommitTranAsync();

    /// <summary>
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    Task<List<BaseFieldInfo>> GetFields(string tableName, bool cache = false);

    /// <summary>
    /// </summary>
    /// <param name="cache"></param>
    /// <returns></returns>
    Task<List<BaseTableInfo>> GetTables(bool cache = false);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task RollbackTranAsync();
}