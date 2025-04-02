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
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// 换班申请分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class PageShiftSchedule(ILogger<PageShiftSchedule> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<SearchShiftScheduleArgs, PageResult<ShiftScheduleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 换班申请分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<ShiftScheduleResult>> Handle(SearchShiftScheduleArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ShiftSchedule.PageShiftSchedule");

        InitUser(httpContextAccessor);

        var args = mapper.Map<SearchShiftScheduleEntityArgs>(request);

        args.Args.RemoveAll(x => x.Field == "CompanyId");
        args.Args.Add(new SearchItem()
        {
            Field = "CompanyId",
            Value = user?.Company?.Id ?? string.Empty,
        });

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<ShiftScheduleResult>>(list);

        var result = new PageResult<ShiftScheduleResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }

    /// <summary>
    /// 获取换班申请用户信息
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