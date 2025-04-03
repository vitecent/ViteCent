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
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// 获取角色权限仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetBaseRolePermission(ILogger<GetBaseRolePermission> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetBaseRolePermissionArgs, DataResult<BaseRolePermissionResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取角色权限
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<BaseRolePermissionResult>> Handle(GetBaseRolePermissionArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseRolePermission.GetBaseRolePermission");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var args = mapper.Map<GetBaseRolePermissionEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<BaseRolePermissionResult>(500, "数据不存在");

        var dto = mapper.Map<BaseRolePermissionResult>(entity);

        return new DataResult<BaseRolePermissionResult>(dto);
    }
}