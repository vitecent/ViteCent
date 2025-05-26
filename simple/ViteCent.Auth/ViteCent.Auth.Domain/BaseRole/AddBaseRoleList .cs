/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRole;

/// <summary>
/// 批量新增角色信息领域
/// </summary>
/// <param name="logger"></param>
public class AddBaseRoleList(
    ILogger<AddBaseRoleList> logger)
    : BaseDomain<AddBaseRoleEntity>, IRequestHandler<AddBaseRoleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 批量新增角色信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseRoleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRole.AddBaseRoleList");

        return await base.AddAsync(request.Items);
    }
}