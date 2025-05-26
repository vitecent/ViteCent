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

        if (entity == null)
            return new BaseResult(500, "公司信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        entity.Abbreviation = request.Abbreviation;
        entity.Address = request.Address;
        entity.City = request.City;
        entity.Code = request.Code;
        entity.Color = request.Color;
        entity.Country = request.Country;
        entity.Description = request.Description;
        entity.Email = request.Email;
        entity.EstablishDate = request.EstablishDate;
        entity.Industry = request.Industry;
        entity.LegalPerson = request.LegalPerson;
        entity.LegalPhone = request.LegalPhone;
        entity.Level = request.Level;
        entity.Logo = request.Logo;
        entity.Name = request.Name;
        entity.ParentId = request.ParentId;
        entity.Province = request.Province;
        entity.Status = request.Status;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseCompany.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}