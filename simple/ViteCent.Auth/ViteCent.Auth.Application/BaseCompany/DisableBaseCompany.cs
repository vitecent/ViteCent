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
/// 禁用公司信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class DisableBaseCompany(
    ILogger<DisableBaseCompany> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<DisableBaseCompanyArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 禁用公司信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DisableBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseCompany.DisableBaseCompany");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<GetBaseCompanyEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "公司信息不存在");

        if (entity.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司信息已禁用");

        entity.Status = (int)StatusEnum.Disable;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseCompany.OverrideTopic(mediator, TopicEnum.Disable, entity, cancellationToken);

        return result;
    }
}