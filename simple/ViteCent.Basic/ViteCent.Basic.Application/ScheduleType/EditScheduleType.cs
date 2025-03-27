#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
    /// <param name="companyInvoke"></param>
    /// <param name="departmentInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditScheduleType(ILogger<EditScheduleType> logger, 
    IMapper mapper, 
    IMediator mediator, 
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditScheduleTypeArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditScheduleTypeArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ScheduleType.EditScheduleType");

        InitUser(httpContextAccessor);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var hasCompanyArgs = new GetBaseCompanyArgs
        {
            Id = request.CompanyId,
        };

        var hasCompany = await companyInvoke.InvokePostAsync("Auth", "BaseCompany/Get", hasCompanyArgs, user?.Token ?? string.Empty);

        if (!hasCompany.Success)
            return hasCompany;

        if (hasCompany.Data == null)
            return new BaseResult(500, "公司不存在");

        if (hasCompany.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司已禁用");

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var hasDepartmentArgs = new GetBaseDepartmentArgs
        {
            CompanyId = request.CompanyId,
            Id = request.DepartmentId,
        };

        var hasDepartment = await departmentInvoke.InvokePostAsync("Auth", "BaseDepartment/Get", hasDepartmentArgs, user?.Token ?? string.Empty);

        if (!hasDepartment.Success)
            return hasDepartment;

        if (hasDepartment.Data == null)
            return new BaseResult(500, "部门不存在");

        if (hasDepartment.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "部门已禁用");

        var result = await OverrideHandle(request, cancellationToken);

        if (!result.Success)
            return result;

        var args = mapper.Map<GetScheduleTypeEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        entity.Code = request.Code;
        entity.Description = request.Description;
        entity.EndTime = request.EndTime;
        entity.Name = request.Name;
        entity.Overnight = request.Overnight;
        entity.StartTime = request.StartTime;
        entity.Status = request.Status;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    private void InitUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();
    }
}