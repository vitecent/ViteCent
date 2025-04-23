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
/// 编辑用户角色
/// </summary>
/// <param name="logger"></param>
public class EditBaseUserRole(
    ILogger<EditBaseUserRole> logger)
    : BaseDomain<BaseUserRoleEntity>, IRequestHandler<BaseUserRoleEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 编辑用户角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(BaseUserRoleEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUserRole.EditBaseUserRole");

        return await base.EditAsync(request);
    }
}