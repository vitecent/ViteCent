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
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// 基础排班分页应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageScheduleType(
    ILogger<PageScheduleType> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SearchScheduleTypeArgs, PageResult<ScheduleTypeResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 基础排班分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<ScheduleTypeResult>> Handle(SearchScheduleTypeArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ScheduleType.PageScheduleType");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchScheduleTypeEntityArgs>(request);

        args.AddCompanyId(user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<ScheduleTypeResult>>(list);

        var result = new PageResult<ScheduleTypeResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}