/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDepartment;

/// <summary>
/// 删除部门信息领域
/// </summary>
/// <param name="logger"></param>
public class DeleteBaseDepartment(
    ILogger<DeleteBaseDepartment> logger)
    : BaseDomain<DeleteBaseDepartmentEntity>, IRequestHandler<DeleteBaseDepartmentEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 删除部门信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseDepartmentEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDepartment.DeleteBaseDepartment");

        return await DeleteAsync(request);
    }
}