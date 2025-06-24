/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUserRole;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseUserRole(ILogger<HasBaseUserRole> logger)
    : BaseDomain<BaseUserRoleEntity>, IRequestHandler<HasBaseUserRoleEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseUserRoleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUserRole.HasBaseUserRole");

        var query = Client.Query<BaseUserRoleEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.RoleId))
            query.Where(x => x.RoleId == request.RoleId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "数据重复");

        return new BaseResult();
    }
}