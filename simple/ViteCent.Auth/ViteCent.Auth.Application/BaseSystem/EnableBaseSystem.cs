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
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseSystem;

/// <summary>
/// 启用系统信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EnableBaseSystem(
    ILogger<EnableBaseSystem> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EnableBaseSystemArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 启用系统信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EnableBaseSystemArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseSystem.EnableBaseSystem");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<GetBaseSystemEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "系统信息不存在");

        if (entity.Status == (int)StatusEnum.Enable)
            return new BaseResult(500, "系统信息已启用");

        entity.Status = (int)StatusEnum.Enable;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseSystem.OverrideTopic(mediator, TopicEnum.Enable, entity, cancellationToken);

        return result;
    }
}