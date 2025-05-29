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
using ViteCent.Basic.Data.BasePost;
using ViteCent.Basic.Entity.BasePost;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.BasePost;

/// <summary>
/// 获取职位信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetBasePost(
    ILogger<GetBasePost> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetBasePostArgs, DataResult<BasePostResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取职位信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<BasePostResult>> Handle(GetBasePostArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.BasePost.GetBasePost");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var args = mapper.Map<GetBasePostEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new DataResult<BasePostResult>(500, "职位信息不存在");

        var dto = mapper.Map<BasePostResult>(entity);

        return new DataResult<BasePostResult>(dto);
    }
}