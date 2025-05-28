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
using ViteCent.Basic.Data.BasePost;
using ViteCent.Basic.Entity.BasePost;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.BasePost;

/// <summary>
/// 职位信息分页应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class PageBasePost(
    ILogger<PageBasePost> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SearchBasePostArgs, PageResult<BasePostResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 职位信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BasePostResult>> Handle(SearchBasePostArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.BasePost.PageBasePost");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchBasePostEntityArgs>(request);

        OverrideHandle(args, user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BasePostResult>>(list);

        var result = new PageResult<BasePostResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}