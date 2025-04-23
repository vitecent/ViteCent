/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 新增排班信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class AddSchedule(
    ILogger<AddSchedule> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AddScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增排班信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddScheduleArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.AddSchedule");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(companyId))
            companyId = request.CompanyId;

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var entity = mapper.Map<AddScheduleEntity>(request);

        entity.Id = await cache.GetIdAsync(companyId, "Schedule");

        entity.Creator = user?.Name ?? string.Empty;
        entity.CreateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        if (!result.Success)
            return result;

        result.Message = entity.Id;

        await OverrideTopic(mediator, TopicEnum.Add, entity, cancellationToken);

        return result;
    }
}