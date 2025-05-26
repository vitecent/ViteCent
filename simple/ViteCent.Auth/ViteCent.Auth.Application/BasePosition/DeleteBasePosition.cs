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
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BasePosition;

/// <summary>
/// 删除职位信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class DeleteBasePosition(ILogger<DeleteBasePosition> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteBasePositionArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 删除职位信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBasePositionArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BasePosition.DeleteBasePosition");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var getArgs = mapper.Map<GetBasePositionEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "职位信息不存在");

        var args = mapper.Map<DeleteBasePositionEntity>(entity);

        var result = await mediator.Send(args, cancellationToken);

        await AddBasePosition.OverrideTopic(mediator, TopicEnum.Delete, entity, cancellationToken);

        return result;
    }
}