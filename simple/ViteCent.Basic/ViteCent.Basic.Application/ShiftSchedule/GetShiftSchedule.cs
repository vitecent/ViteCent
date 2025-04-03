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
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// 获取换班申请仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetShiftSchedule(ILogger<GetShiftSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetShiftScheduleArgs, DataResult<ShiftScheduleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取换班申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<ShiftScheduleResult>> Handle(GetShiftScheduleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ShiftSchedule.GetShiftSchedule");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var args = mapper.Map<GetShiftScheduleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<ShiftScheduleResult>(500, "数据不存在");

        var dto = mapper.Map<ShiftScheduleResult>(entity);

        return new DataResult<ShiftScheduleResult>(dto);
    }
}