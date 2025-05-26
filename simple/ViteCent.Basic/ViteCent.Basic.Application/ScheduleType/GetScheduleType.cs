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
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// 获取基础排班应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetScheduleType(
    ILogger<GetScheduleType> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetScheduleTypeArgs, DataResult<ScheduleTypeResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取基础排班
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<ScheduleTypeResult>> Handle(GetScheduleTypeArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ScheduleType.GetScheduleType");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var args = mapper.Map<GetScheduleTypeEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<ScheduleTypeResult>(500, "基础排班不存在");

        var dto = mapper.Map<ScheduleTypeResult>(entity);

        return new DataResult<ScheduleTypeResult>(dto);
    }
}