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
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseDictionary;

/// <summary>
/// 编辑字典信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseDictionary(
    ILogger<EditBaseDictionary> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseDictionaryArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑字典信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseDictionaryArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseDictionary.EditBaseDictionary");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var getArgs = mapper.Map<GetBaseDictionaryEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "字典信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        entity.Abbreviation = request.Abbreviation;
        entity.Code = request.Code;
        entity.Color = request.Color;
        entity.Description = request.Description;
        entity.Level = request.Level;
        entity.Name = request.Name;
        entity.ParentId = request.ParentId;
        entity.Status = request.Status;
        entity.Value = request.Value;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseDictionary.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}