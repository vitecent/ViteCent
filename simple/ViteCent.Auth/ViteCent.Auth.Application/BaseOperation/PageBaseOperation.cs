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
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// 操作信息分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageBaseOperation(ILogger<PageBaseOperation> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<SearchBaseOperationArgs, PageResult<BaseOperationResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 操作信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BaseOperationResult>> Handle(SearchBaseOperationArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseOperation.PageBaseOperation");

        InitUser(httpContextAccessor);

        var args = mapper.Map<SearchBaseOperationEntityArgs>(request);

        args.Args.RemoveAll(x => x.Field == "CompanyId");
        args.Args.Add(new SearchItem()
        {
            Field = "CompanyId",
            Value = user?.Company?.Id ?? string.Empty,
        });

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BaseOperationResult>>(list);

        var result = new PageResult<BaseOperationResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }

    /// <summary>
    /// 获取操作信息用户信息
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