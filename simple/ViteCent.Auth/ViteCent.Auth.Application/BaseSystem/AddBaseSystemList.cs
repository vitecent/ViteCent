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
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseSystem;

/// <summary>
/// 新增系统信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class AddBaseSystemList(
    ILogger<AddBaseSystemList> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AddBaseSystemListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增系统信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseSystemListArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseSystem.AddBaseSystemList");

        user = httpContextAccessor.InitUser();

        var check = await AddBaseSystem.OverrideHandle(mediator, request, user, cancellationToken);

        if (!check.Success)
            return check;

        var entitys = new AddBaseSystemEntityListArgs
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            if (string.IsNullOrWhiteSpace(companyId))
                companyId = item.CompanyId;

            var entity = mapper.Map<AddBaseSystemEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "BaseSystem");

            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        var result = await mediator.Send(entitys, cancellationToken);

        foreach (var entity in entitys.Items)
            await AddBaseSystem.OverrideTopic(mediator, TopicEnum.Add, entity, cancellationToken);

        return result;
    }
}