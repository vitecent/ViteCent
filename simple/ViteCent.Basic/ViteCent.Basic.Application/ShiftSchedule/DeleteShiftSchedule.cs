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
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// 删除换班申请应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class DeleteShiftSchedule(ILogger<DeleteShiftSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteShiftScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 删除换班申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteShiftScheduleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ShiftSchedule.DeleteShiftSchedule");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var getArgs = mapper.Map<GetShiftScheduleEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "换班申请不存在");

        var args = mapper.Map<DeleteShiftScheduleEntity>(entity);

        var result = await mediator.Send(args, cancellationToken);

        await AddShiftSchedule.OverrideTopic(mediator, TopicEnum.Delete, entity, cancellationToken);

        return result;
    }
}