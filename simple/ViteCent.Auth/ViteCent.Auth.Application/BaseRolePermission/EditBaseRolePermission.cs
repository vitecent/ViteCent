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
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// 编辑角色权限仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseRolePermission(ILogger<EditBaseRolePermission> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditBaseRolePermissionArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑角色权限
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseRolePermissionArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseRolePermission.EditBaseRolePermission");

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

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.SystemId))
        {
            var hasSystemArgs = new GetBaseSystemEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.SystemId,
            };

            var hasSystem = await mediator.Send(hasSystemArgs, cancellationToken);

            if (hasSystem == null)
                return new BaseResult(500, "系统不存在");

            if (hasSystem.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "系统已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.SystemId) && !string.IsNullOrWhiteSpace(request.ResourceId))
        {
            var hasResourceArgs = new GetBaseResourceEntityArgs
            {
                CompanyId = request.CompanyId,
                SystemId = request.SystemId,
                Id = request.ResourceId,
            };

            var hasResource = await mediator.Send(hasResourceArgs, cancellationToken);

            if (hasResource == null)
                return new BaseResult(500, "资源不存在");

            if (hasResource.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "资源已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.SystemId) && !string.IsNullOrWhiteSpace(request.ResourceId) && !string.IsNullOrWhiteSpace(request.OperationId))
        {
            var hasOperationArgs = new GetBaseOperationEntityArgs
            {
                CompanyId = request.CompanyId,
                SystemId = request.SystemId,
                ResourceId = request.ResourceId,
                Id = request.OperationId,
            };

            var hasOperation = await mediator.Send(hasOperationArgs, cancellationToken);

            if (hasOperation == null)
                return new BaseResult(500, "操作不存在");

            if (hasOperation.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "操作已禁用");
        }

        var preResult = await OverrideHandle(request, cancellationToken);

        if (!preResult.Success)
            return preResult;

        var args = mapper.Map<GetBaseRolePermissionEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "数据不存在或无权限");

        var result = await OverrideHandle(entity, cancellationToken);

        if (!result.Success)
            return result;

        entity.OperationId = request.OperationId;
        entity.ResourceId = request.ResourceId;
        entity.RoleId = request.RoleId;
        entity.Status = request.Status;
        entity.SystemId = request.SystemId;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }

    /// <summary>
    /// 获取角色权限用户信息
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