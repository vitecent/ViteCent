/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRolePermission;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseRolePermission(ILogger<HasBaseRolePermission> logger) : BaseDomain<BaseRolePermissionEntity>,
    IRequestHandler<HasBaseRolePermissionEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseRolePermissionEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRolePermission.HasBaseRolePermission");

        var query = Client.Query<BaseRolePermissionEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.RoleId))
            query.Where(x => x.RoleId == request.RoleId);

        if (!string.IsNullOrWhiteSpace(request.SystemId))
            query.Where(x => x.SystemId == request.SystemId);

        if (!string.IsNullOrWhiteSpace(request.ResourceId))
            query.Where(x => x.ResourceId == request.ResourceId);

        if (!string.IsNullOrWhiteSpace(request.OperationId))
            query.Where(x => x.OperationId == request.OperationId);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "编码 或 名称 重复");

        return new BaseResult();
    }
}