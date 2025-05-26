/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseResource;

/// <summary>
/// 删除资源信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class DeleteBaseResource(ILogger<DeleteBaseResource> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteBaseResourceArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 删除资源信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseResourceArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseResource.DeleteBaseResource");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var getArgs = mapper.Map<GetBaseResourceEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "资源信息不存在");

        var args = mapper.Map<DeleteBaseResourceEntity>(entity);

        var result = await mediator.Send(args, cancellationToken);

        await AddBaseResource.OverrideTopic(mediator, TopicEnum.Delete, entity, cancellationToken);

        return result;
    }
}