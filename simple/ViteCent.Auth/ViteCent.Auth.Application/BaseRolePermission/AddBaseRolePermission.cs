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
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// 新增角色权限仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class AddBaseRolePermission(ILogger<AddBaseRolePermission> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddBaseRolePermissionArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增角色权限
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseRolePermissionArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseRolePermission.AddBaseRolePermission");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        var hasRole = await mediator.CheckRole(request.CompanyId, request.RoleId);

        if (hasRole.Success)
            return hasRole;

        var hasSystem = await mediator.CheckSystem(request.CompanyId, request.SystemId);

        if (hasSystem.Success)
            return hasSystem;

        var hasResource = await mediator.CheckResource(request.CompanyId, request.SystemId, request.ResourceId);;

        if (hasResource.Success)
            return hasResource;

        var hasOperation = await mediator.CheckOperation(request.CompanyId, request.SystemId, request.ResourceId, request.OperationId);

        if (hasOperation.Success)
            return hasOperation;

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var entity = mapper.Map<AddBaseRolePermissionEntity>(request);

        entity.Id = await cache.GetIdAsync(companyId, "BaseRolePermission");

        entity.Creator = user?.Name ?? string.Empty;
        entity.CreateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        if (!result.Success)
            return result;

        return new BaseResult(entity.Id);
    }
}