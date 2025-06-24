/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUserRole;

/// <summary>
/// 批量用户角色判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseUserRoleList(ILogger<HasBaseUserRoleList> logger) : BaseDomain<BaseUserRoleEntity>,
    IRequestHandler<HasBaseUserRoleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// 批量用户角色判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseUserRoleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUserRole.HasBaseUserRole");

        var query = Client.Query<BaseUserRoleEntity>();

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.Id));

        if (request.DepartmentIds.Count > 0)
            query.Where(x => request.DepartmentIds.Contains(x.DepartmentId));

        if (request.RoleIds.Count > 0)
            query.Where(x => request.RoleIds.Contains(x.RoleId));

        if (request.UserIds.Count > 0)
            query.Where(x => request.UserIds.Contains(x.UserId));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "数据重复");

        return new BaseResult();
    }
}