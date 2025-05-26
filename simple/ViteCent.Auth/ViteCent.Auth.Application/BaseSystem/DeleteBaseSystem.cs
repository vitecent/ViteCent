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
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseSystem;

/// <summary>
/// 删除系统信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class DeleteBaseSystem(ILogger<DeleteBaseSystem> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteBaseSystemArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 删除系统信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseSystemArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseSystem.DeleteBaseSystem");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var getArgs = mapper.Map<GetBaseSystemEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "系统信息不存在");

        var args = mapper.Map<DeleteBaseSystemEntity>(entity);

        var result = await mediator.Send(args, cancellationToken);

        await AddBaseSystem.OverrideTopic(mediator, TopicEnum.Delete, entity, cancellationToken);

        return result;
    }
}