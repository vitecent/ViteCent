/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.ScheduleType;

/// <summary>
/// 获取基础排班
/// </summary>
/// <param name="logger"></param>
public class GetScheduleType(
    ILogger<GetScheduleType> logger)
    : BaseDomain<ScheduleTypeEntity>, IRequestHandler<GetScheduleTypeEntityArgs, ScheduleTypeEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 获取基础排班
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ScheduleTypeEntity> Handle(GetScheduleTypeEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ScheduleType.GetScheduleType");

        var query = Client.Query<ScheduleTypeEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        return await query.FirstAsync(cancellationToken);
    }
}