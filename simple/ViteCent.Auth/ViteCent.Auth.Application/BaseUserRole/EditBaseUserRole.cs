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
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUserRole;

/// <summary>
/// 编辑用户角色应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseUserRole(
    ILogger<EditBaseUserRole> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseUserRoleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑用户角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseUserRoleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUserRole.EditBaseUserRole");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var getArgs = mapper.Map<GetBaseUserRoleEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "用户角色不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        entity.Color = request.Color;
        entity.RoleId = request.RoleId;
        entity.Status = request.Status;
        entity.UserId = request.UserId;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseUserRole.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}