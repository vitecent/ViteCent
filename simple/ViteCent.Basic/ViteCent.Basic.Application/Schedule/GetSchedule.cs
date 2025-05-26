/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 获取排班信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetSchedule(
    ILogger<GetSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetScheduleArgs, DataResult<ScheduleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取排班信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<ScheduleResult>> Handle(GetScheduleArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.GetSchedule");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var args = mapper.Map<GetScheduleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<ScheduleResult>(500, "排班信息不存在");

        var dto = mapper.Map<ScheduleResult>(entity);

        return new DataResult<ScheduleResult>(dto);
    }
}