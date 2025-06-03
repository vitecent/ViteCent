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

// 引入角色权限相关的数据模型
using ViteCent.Auth.Entity.BaseRolePermission;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Auth.Domain.BaseRolePermission;

/// <summary>
/// 获取角色权限领域服务类
/// </summary>
/// <remarks>
/// 该类负责处理获取单个角色权限的业务逻辑，包括：
/// 1. 根据角色权限标识查询角色权限详细信息
/// 2. 返回查询结果
/// </remarks>
/// <param name="logger">日志记录器实例</param>
public class GetBaseRolePermission(
    // 注入日志记录器
    ILogger<GetBaseRolePermission> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseDomain<BaseRolePermissionEntity>, IRequestHandler<GetBaseRolePermissionEntityArgs, BaseRolePermissionEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 处理获取角色权限的请求
    /// </summary>
    /// <param name="request">包含角色权限标识的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>角色权限模型信息</returns>
    /// <remarks>
    /// 该方法执行以下步骤：
    /// 1. 记录方法调用日志
    /// 2. 构建查询条件
    /// 3. 执行查询并返回第一条匹配记录
    /// </remarks>
    public async Task<BaseRolePermissionEntity> Handle(GetBaseRolePermissionEntityArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRolePermission.GetBaseRolePermission");

        // 初始化查询对象
        var query = Client.Query<BaseRolePermissionEntity>();

        // 如果请求中包含标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        // 如果请求中包含公司标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        // 如果请求中包含角色标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.RoleId))
            query.Where(x => x.RoleId == request.RoleId);

        // 如果请求中包含核心标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.SystemId))
            query.Where(x => x.SystemId == request.SystemId);

        // 如果请求中包含资源标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.ResourceId))
            query.Where(x => x.ResourceId == request.ResourceId);

        // 如果请求中包含操作标识，则添加查询条件
        if (!string.IsNullOrWhiteSpace(request.OperationId))
            query.Where(x => x.OperationId == request.OperationId);

        // 执行异步查询，返回第一条匹配记录
        return await query.FirstAsync(cancellationToken);
    }
}