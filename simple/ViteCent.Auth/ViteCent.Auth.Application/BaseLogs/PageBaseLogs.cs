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
using ViteCent.Auth.Data.BaseLogs;
using ViteCent.Auth.Entity.BaseLogs;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseLogs;

/// <summary>
/// 日志信息分页应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class PageBaseLogs(
    ILogger<PageBaseLogs> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SearchBaseLogsArgs, PageResult<BaseLogsResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 日志信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BaseLogsResult>> Handle(SearchBaseLogsArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseLogs.PageBaseLogs");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchBaseLogsEntityArgs>(request);

        OverrideHandle(args, user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BaseLogsResult>>(list);

        var result = new PageResult<BaseLogsResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}