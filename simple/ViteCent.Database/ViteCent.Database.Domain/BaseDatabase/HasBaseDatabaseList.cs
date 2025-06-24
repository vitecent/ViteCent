/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

// 引入数据库信息相关的数据参数

// 引入数据库信息相关的数据模型
using ViteCent.Database.Entity.BaseDatabase;

#endregion 引入命名空间

namespace ViteCent.Database.Domain.BaseDatabase;

/// <summary>
/// 批量数据库信息判重处理类 </summary> <remarks> 该类用于处理批量数据库信息的判重逻辑，包括：
/// 1. 检查数据库信息编码是否重复
/// 2. 检查数据库信息名称是否重复
/// 3. 返回判重结果 </remarks> <param name="logger">日志记录器实例</param>
public class HasBaseDatabaseList(
    // 注入日志记录器
    ILogger<HasBaseDatabaseList> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseDomain<BaseDatabaseEntity>, IRequestHandler<HasBaseDatabaseEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Database";

    /// <summary>
    /// 处理批量数据库信息判重请求
    /// </summary>
    /// <param name="request">包含待检查的数据库信息编码和名称列表的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>判重结果，包含状态码和提示信息</returns>
    /// <remarks>
    /// 该方法执行以下步骤：
    /// 1. 记录方法调用日志
    /// 2. 构建查询条件，检查编码和名称是否存在重复
    /// 3. 执行查询并返回结果
    /// </remarks>
    public async Task<BaseResult> Handle(HasBaseDatabaseEntityListArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Database.Domain.BaseDatabase.HasBaseDatabase");

        // 初始化查询对象
        var query = Client.Query<BaseDatabaseEntity>();

        // 移除空白的公司标识
        request.CompanyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        // 如果公司标识列表不为空，添加查询条件
        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.Id));

        // 执行查询，获取匹配记录数
        var entity = await query.CountAsync(cancellationToken);

        // 如果存在匹配记录，返回错误结果
        if (entity > 0)
            return new BaseResult(500, "数据库信息重复");

        // 如果没有匹配记录，返回成功结果
        return new BaseResult();
    }
}