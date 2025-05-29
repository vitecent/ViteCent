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
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// 编辑操作信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseOperation(
    ILogger<EditBaseOperation> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseOperationArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑操作信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseOperationArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseOperation.EditBaseOperation");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var getArgs = mapper.Map<GetBaseOperationEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "操作信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if (request.Abbreviation is not null)
            entity.Abbreviation = request.Abbreviation;

        if (request.Code is not null)
            entity.Code = request.Code;

        if (request.Color is not null)
            entity.Color = request.Color;

        if (request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        if (request.Description is not null)
            entity.Description = request.Description;

        entity.Name = request.Name;

        entity.ResourceId = request.ResourceId;

        if (request.ResourceName is not null)
            entity.ResourceName = request.ResourceName;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.SystemId = request.SystemId;

        if (request.SystemName is not null)
            entity.SystemName = request.SystemName;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseOperation.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}