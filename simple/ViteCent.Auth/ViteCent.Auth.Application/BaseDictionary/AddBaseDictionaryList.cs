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
using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Auth.Entity.BaseDictionary;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseDictionary;

/// <summary>
/// 新增字典信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class AddBaseDictionaryList(
    ILogger<AddBaseDictionaryList> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AddBaseDictionaryListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增字典信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseDictionaryListArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseDictionary.AddBaseDictionaryList");

        user = httpContextAccessor.InitUser();

        var check = await AddBaseDictionary.OverrideHandle(mediator, request, user, cancellationToken);

        if (!check.Success)
            return check;

        var entitys = new AddBaseDictionaryEntityListArgs
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            if (string.IsNullOrWhiteSpace(companyId))
                companyId = item.CompanyId;

            var entity = mapper.Map<AddBaseDictionaryEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "BaseDictionary");

            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        var result = await mediator.Send(entitys, cancellationToken);

        foreach (var entity in entitys.Items)
            await AddBaseDictionary.OverrideTopic(mediator, TopicEnum.Add, entity, cancellationToken);

        return result;
    }
}