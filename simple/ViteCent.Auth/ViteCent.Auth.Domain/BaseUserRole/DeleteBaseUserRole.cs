/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
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
/// 删除用户角色
/// </summary>
/// <param name="logger"></param>
public class DeleteBaseUserRole(
    ILogger<DeleteBaseUserRole> logger)
    : BaseDomain<DeleteBaseUserRoleEntity>, IRequestHandler<DeleteBaseUserRoleEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseUserRoleEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUserRole.DeleteBaseUserRole");

        return await DeleteAsync(request);
    }
}