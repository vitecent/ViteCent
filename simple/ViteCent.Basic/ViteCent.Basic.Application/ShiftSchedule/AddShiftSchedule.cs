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
using System.Security.Claims;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// 新增换班申请仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
   /// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class AddShiftSchedule(ILogger<AddShiftSchedule> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddShiftScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增换班申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddShiftScheduleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ShiftSchedule.AddShiftSchedule");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);;

        if (hasCompany.Success)
            return hasCompany;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await departmentInvoke.CheckDepartment(request.CompanyId, request.DepartmentId, user?.Token ?? string.Empty);

        if (hasDepartment.Success)
            return hasDepartment;

        var hasUser = await userInvoke.CheckUser(request.CompanyId, request.DepartmentId, request.UserId, user?.Token ?? string.Empty);

        if (hasUser.Success)
            return hasUser;

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var entity = mapper.Map<AddShiftScheduleEntity>(request);

        entity.Id = await cache.GetIdAsync(companyId, "ShiftSchedule");

        entity.Creator = user?.Name ?? string.Empty;
        entity.CreateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

         var result = await mediator.Send(entity, cancellationToken);

        if (!result.Success)
            return result;

        return new BaseResult(entity.Id);
    }
}