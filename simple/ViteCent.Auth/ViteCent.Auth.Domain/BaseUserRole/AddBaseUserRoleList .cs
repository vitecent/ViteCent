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
/// 批量新增用户角色领域
/// </summary>
/// <param name="logger"></param>
public class AddBaseUserRoleList(
    ILogger<AddBaseUserRoleList> logger)
    : BaseDomain<AddBaseUserRoleEntity>, IRequestHandler<AddBaseUserRoleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 批量新增用户角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseUserRoleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUserRole.AddBaseUserRoleList");

        return await base.AddAsync(request.Items);
    }
}