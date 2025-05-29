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
using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.RepairSchedule;

/// <summary>
/// 编辑补卡申请应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditRepairSchedule(
    ILogger<EditRepairSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditRepairScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑补卡申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditRepairScheduleArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.RepairSchedule.EditRepairSchedule");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var args = mapper.Map<GetRepairScheduleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "补卡申请不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if (request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        if (request.DepartmentName is not null)
            entity.DepartmentName = request.DepartmentName;

        if (request.Remark is not null)
            entity.Remark = request.Remark;

        entity.RepairTime = request.RepairTime;

        entity.RepairType = request.RepairType;

        entity.ScheduleId = request.ScheduleId;

        if (request.ScheduleName is not null)
            entity.ScheduleName = request.ScheduleName;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.UserId = request.UserId;

        if (request.UserName is not null)
            entity.UserName = request.UserName;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddRepairSchedule.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}