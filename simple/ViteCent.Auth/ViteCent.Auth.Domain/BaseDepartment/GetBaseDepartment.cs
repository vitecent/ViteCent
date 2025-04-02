/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDepartment;

/// <summary>
/// 获取部门信息
/// </summary>
/// <param name="logger"></param>
public class GetBaseDepartment(ILogger<GetBaseDepartment> logger) : BaseDomain<BaseDepartmentEntity>, IRequestHandler<GetBaseDepartmentEntityArgs, BaseDepartmentEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 获取部门信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseDepartmentEntity> Handle(GetBaseDepartmentEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDepartment.GetBaseDepartment");

        var query = Client.Query<BaseDepartmentEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        return await query.FirstAsync();
    }
}