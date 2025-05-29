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
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 编辑用户信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseUser(
    ILogger<EditBaseUser> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseUserArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseUserArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.EditBaseUser");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var getArgs = mapper.Map<GetBaseUserEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "用户信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if (request.Avatar is not null)
            entity.Avatar = request.Avatar;

        if (request.Birthday.HasValue)
            entity.Birthday = request.Birthday.Value;

        if (request.Color is not null)
            entity.Color = request.Color;

        entity.CompanyId = request.CompanyId;

        if (request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        entity.DepartmentId = request.DepartmentId;

        if (request.DepartmentName is not null)
            entity.DepartmentName = request.DepartmentName;

        if (request.Description is not null)
            entity.Description = request.Description;

        if (request.Email is not null)
            entity.Email = request.Email;

        if (request.Finger is not null)
            entity.Finger = request.Finger;

        if (request.Gender.HasValue)
            entity.Gender = request.Gender.Value;

        if (request.IdCard is not null)
            entity.IdCard = request.IdCard;

        if (request.IsSuper.HasValue)
            entity.IsSuper = request.IsSuper.Value;

        if (request.Nickname is not null)
            entity.Nickname = request.Nickname;

        if (request.Password is not null)
            entity.Password = request.Password;

        if (request.Phone is not null)
            entity.Phone = request.Phone;

        if (request.PositionId is not null)
            entity.PositionId = request.PositionId;

        if (request.PositionName is not null)
            entity.PositionName = request.PositionName;

        if (request.RealName is not null)
            entity.RealName = request.RealName;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.Username = request.Username;

        if (request.UserNo is not null)
            entity.UserNo = request.UserNo;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseUser.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}