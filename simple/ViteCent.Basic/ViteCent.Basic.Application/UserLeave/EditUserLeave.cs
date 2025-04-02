/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
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
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.UserLeave;

/// <summary>
/// 编辑请假申请仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditUserLeave(ILogger<EditUserLeave> logger,
    IMapper mapper, 
    IMediator mediator, 
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditUserLeaveArgs, BaseResult>
{
    /// <summary>
    /// 用户密钥
    /// </summary>
    private string token = string.Empty;

    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑请假申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditUserLeaveArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.UserLeave.EditUserLeave");

        InitUser(httpContextAccessor);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
        {
            var hasCompanyArgs = new GetBaseCompanyArgs
            {
                Id = request.CompanyId,
            };

            var hasCompany = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Get", hasCompanyArgs, token);

            if (!hasCompany.Success)
                return hasCompany;

            if (hasCompany.Data == null)
                return new BaseResult(500, "公司不存在");

            if (hasCompany.Data.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "公司已禁用");
        }

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.DepartmentId))
        {
            var hasDepartmentArgs = new GetBaseDepartmentArgs
            {
                CompanyId = request.CompanyId,
                Id = request.DepartmentId,
            };

            var hasDepartment = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Get", hasDepartmentArgs, token);

            if (!hasDepartment.Success)
                return hasDepartment;

            if (hasDepartment.Data == null)
                return new BaseResult(500, "部门不存在");

            if (hasDepartment.Data.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "部门已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.DepartmentId) && !string.IsNullOrWhiteSpace(request.UserId))
        {
            var hasUserArgs = new GetBaseUserArgs
            {
                CompanyId = request.CompanyId,
                DepartmentId = request.DepartmentId,
                Id = request.UserId,
            };

            var hasUser = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Get", hasUserArgs, token);

            if (!hasUser.Success)
                return hasUser;

            if (hasUser.Data == null)
                return new BaseResult(500, "用户不存在");

            if (hasUser.Data.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "用户已禁用");
        }

        var preResult = await OverrideHandle(request, cancellationToken);

        if (!preResult.Success)
            return preResult;

        var args = mapper.Map<GetUserLeaveEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "数据不存在或无权限");

        var result = await OverrideHandle(entity, cancellationToken);

        if (!result.Success)
            return result;

        entity.EndTime = request.EndTime;
        entity.Remark = request.Remark;
        entity.StartTime = request.StartTime;
        entity.Status = request.Status;
        entity.UserId = request.UserId;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }

    /// <summary>
    /// 获取请假申请用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    private void InitUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;

        token = context?.Request?.Headers[Const.Token].ToString() ?? string.Empty;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();
    }
}