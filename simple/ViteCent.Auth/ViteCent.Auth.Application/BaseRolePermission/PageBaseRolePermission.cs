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
/// 角色权限分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageBaseRolePermission(ILogger<PageBaseRolePermission> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<SearchBaseRolePermissionArgs, PageResult<BaseRolePermissionResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 角色权限分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BaseRolePermissionResult>> Handle(SearchBaseRolePermissionArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseRolePermission.PageBaseRolePermission");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchBaseRolePermissionEntityArgs>(request);

        args.AddCompanyId(user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BaseRolePermissionResult>>(list);

        var result = new PageResult<BaseRolePermissionResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}