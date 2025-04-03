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
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseRole;

/// <summary>
/// 获取角色信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetBaseRole(ILogger<GetBaseRole> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetBaseRoleArgs, DataResult<BaseRoleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<BaseRoleResult>> Handle(GetBaseRoleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseRole.GetBaseRole");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var args = mapper.Map<GetBaseRoleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<BaseRoleResult>(500, "数据不存在");

        var dto = mapper.Map<BaseRoleResult>(entity);

        return new DataResult<BaseRoleResult>(dto);
    }
}