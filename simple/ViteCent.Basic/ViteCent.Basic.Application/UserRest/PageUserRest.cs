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
using ViteCent.Basic.Data.UserRest;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.UserRest;

/// <summary>
/// 调休申请分页应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageUserRest(
    ILogger<PageUserRest> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SearchUserRestArgs, PageResult<UserRestResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 调休申请分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<UserRestResult>> Handle(SearchUserRestArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.UserRest.PageUserRest");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchUserRestEntityArgs>(request);

        args.AddCompanyId(user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<UserRestResult>>(list);

        var result = new PageResult<UserRestResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}