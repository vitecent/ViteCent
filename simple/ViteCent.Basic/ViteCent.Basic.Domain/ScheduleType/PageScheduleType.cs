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
/// 基础排班分页领域
/// </summary>
/// <param name="logger"></param>
public class PageScheduleType(ILogger<PageScheduleType> logger) : BaseDomain<ScheduleTypeEntity>, IRequestHandler<SearchScheduleTypeEntityArgs, List<ScheduleTypeEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 基础排班分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<ScheduleTypeEntity>> Handle(SearchScheduleTypeEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ScheduleType.PageScheduleType");

        return await base.PageAsync(request);
    }
}