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
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUserRole;

/// <summary>
/// 编辑用户角色仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseUserRole(ILogger<EditBaseUserRole> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditBaseUserRoleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑用户角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseUserRoleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUserRole.EditBaseUserRole");

        InitUser(httpContextAccessor);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
        {
            var hasCompanyArgs = new GetBaseCompanyEntityArgs
            {
                Id = request.CompanyId,
            };

            var hasCompany = await mediator.Send(hasCompanyArgs, cancellationToken);

            if (hasCompany == null)
                return new BaseResult(500, "公司不存在");

            if (hasCompany.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "公司已禁用");
        }

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.DepartmentId))
        {
            var hasDepartmentArgs = new GetBaseDepartmentEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.DepartmentId,
            };

            var hasDepartment = await mediator.Send(hasDepartmentArgs, cancellationToken);

            if (hasDepartment == null)
                return new BaseResult(500, "部门不存在");

            if (hasDepartment.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "部门已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.DepartmentId) && !string.IsNullOrWhiteSpace(request.UserId))
        {
            var hasUserArgs = new GetBaseUserEntityArgs
            {
                CompanyId = request.CompanyId,
                DepartmentId = request.DepartmentId,
                Id = request.UserId,
            };

            var hasUser = await mediator.Send(hasUserArgs, cancellationToken);

            if (hasUser == null)
                return new BaseResult(500, "用户不存在");

            if (hasUser.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "用户已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.RoleId))
        {
            var hasRoleArgs = new GetBaseRoleEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.RoleId,
            };

            var hasRole = await mediator.Send(hasRoleArgs, cancellationToken);

            if (hasRole == null)
                return new BaseResult(500, "角色不存在");

            if (hasRole.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "角色已禁用");
        }

        var preResult = await OverrideHandle(request, cancellationToken);

        if (!preResult.Success)
            return preResult;

        var args = mapper.Map<GetBaseUserRoleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "数据不存在或无权限");

        var result = await OverrideHandle(entity, cancellationToken);

        if (!result.Success)
            return result;

        entity.Color = request.Color;
        entity.RoleId = request.RoleId;
        entity.Status = request.Status;
        entity.UserId = request.UserId;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }

    /// <summary>
    /// 获取用户角色用户信息
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