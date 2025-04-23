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
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseSystem;

/// <summary>
/// 系统信息分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageBaseSystem(
    ILogger<PageBaseSystem> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SearchBaseSystemArgs, PageResult<BaseSystemResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 系统信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BaseSystemResult>> Handle(SearchBaseSystemArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseSystem.PageBaseSystem");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchBaseSystemEntityArgs>(request);

        args.AddCompanyId(user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BaseSystemResult>>(list);

        var result = new PageResult<BaseSystemResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}