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
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseDepartment;

/// <summary>
/// 编辑部门信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseDepartment(
    ILogger<EditBaseDepartment> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseDepartmentArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑部门信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseDepartmentArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseDepartment.EditBaseDepartment");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var getArgs = mapper.Map<GetBaseDepartmentEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "部门信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if(request.Abbreviation is not null)
            entity.Abbreviation = request.Abbreviation;

        if(request.Code is not null)
            entity.Code = request.Code;

        if(request.Color is not null)
            entity.Color = request.Color;

        if(request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        if(request.Description is not null)
            entity.Description = request.Description;

        if(request.Level is not null)
            entity.Level = request.Level;

        if(request.Manager is not null)
            entity.Manager = request.Manager;

        if(request.ManagerPhone is not null)
            entity.ManagerPhone = request.ManagerPhone;

        entity.Name = request.Name;

        if(request.ParentId is not null)
            entity.ParentId = request.ParentId;

        if(request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseDepartment.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}