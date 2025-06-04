/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入资源信息相关的数据模型
using ViteCent.Auth.Entity.BaseResource;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Auth.Domain.BaseResource;

/// <summary>
/// 获取资源信息领域服务类
/// </summary>
/// <remarks>
/// 该类负责处理获取单个资源信息的业务逻辑，包括：
/// 1. 根据资源信息标识查询资源信息详细信息
/// 2. 返回查询结果
/// </remarks>
/// <param name="logger">日志记录器实例</param>
public class GetBaseResource(
    // 注入日志记录器
    ILogger<GetBaseResource> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseDomain<BaseResourceEntity>, IRequestHandler<GetBaseResourceEntityArgs, BaseResourceEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 处理获取资源信息的请求
    /// </summary>
    /// <param name="request">包含资源信息标识的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>资源信息模型信息</returns>
    /// <remarks>
    /// 该方法执行以下步骤：
    /// 1. 记录方法调用日志
    /// 2. 构建查询条件
    /// 3. 执行查询并返回第一条匹配记录
    /// </remarks>
    public async Task<BaseResourceEntity> Handle(GetBaseResourceEntityArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseResource.GetBaseResource");

        // 初始化查询对象
        var query = Client.Query<BaseResourceEntity>();

        // 如果请求中包含标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        // 如果请求中包含公司标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        // 如果请求中包含核心标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.SystemId))
            query.Where(x => x.SystemId == request.SystemId);

        // 执行查询，返回第一条匹配记录
        return await query.FirstAsync(cancellationToken);
    }
}