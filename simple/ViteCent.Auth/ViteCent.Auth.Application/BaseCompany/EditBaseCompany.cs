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
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// 编辑公司信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseCompany(
    ILogger<EditBaseCompany> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseCompanyArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑公司信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseCompany.EditBaseCompany");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var getArgs = mapper.Map<GetBaseCompanyEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "公司信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if (request.Abbreviation is not null)
            entity.Abbreviation = request.Abbreviation;

        if (request.Address is not null)
            entity.Address = request.Address;

        if (request.City is not null)
            entity.City = request.City;

        if (request.Code is not null)
            entity.Code = request.Code;

        if (request.Color is not null)
            entity.Color = request.Color;

        if (request.Country is not null)
            entity.Country = request.Country;

        if (request.Description is not null)
            entity.Description = request.Description;

        if (request.Email is not null)
            entity.Email = request.Email;

        if (request.EstablishDate.HasValue)
            entity.EstablishDate = request.EstablishDate.Value;

        if (request.Industry is not null)
            entity.Industry = request.Industry;

        if (request.LegalPerson is not null)
            entity.LegalPerson = request.LegalPerson;

        if (request.LegalPhone is not null)
            entity.LegalPhone = request.LegalPhone;

        if (request.Level is not null)
            entity.Level = request.Level;

        if (request.Logo is not null)
            entity.Logo = request.Logo;

        entity.Name = request.Name;

        if (request.ParentId is not null)
            entity.ParentId = request.ParentId;

        if (request.Province is not null)
            entity.Province = request.Province;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseCompany.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}