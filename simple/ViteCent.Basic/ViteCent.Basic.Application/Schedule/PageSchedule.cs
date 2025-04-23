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
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 排班信息分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageSchedule(
    ILogger<PageSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SearchScheduleArgs, PageResult<ScheduleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 排班信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<ScheduleResult>> Handle(SearchScheduleArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.PageSchedule");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchScheduleEntityArgs>(request);

        args.AddCompanyId(user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<ScheduleResult>>(list);

        var result = new PageResult<ScheduleResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}