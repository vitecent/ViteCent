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
using ViteCent.Core.Data;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 编辑排班信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class LastSchedule(
    ILogger<LastSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<LastScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑排班信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(LastScheduleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.LastSchedule");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var args = mapper.Map<GetScheduleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "排班信息不存在");

        var result = await OverrideHandle(entity, cancellationToken);

        if (!result.Success)
            return result;

        entity.LastTime = DateTime.Now;
        entity.Status = (int)ScheduleEnum.Last;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }
}