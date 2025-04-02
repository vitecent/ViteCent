/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 用户信息分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageBaseUser(ILogger<PageBaseUser> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<SearchBaseUserArgs, PageResult<BaseUserResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 用户信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BaseUserResult>> Handle(SearchBaseUserArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.PageBaseUser");

        InitUser(httpContextAccessor);

        var args = mapper.Map<SearchBaseUserEntityArgs>(request);

        args.Args.RemoveAll(x => x.Field == "CompanyId");
        args.Args.Add(new SearchItem()
        {
            Field = "CompanyId",
            Value = user?.Company?.Id ?? string.Empty,
        });

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BaseUserResult>>(list);

        var result = new PageResult<BaseUserResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }

    /// <summary>
    /// 获取用户信息用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    private void InitUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();
    }
}