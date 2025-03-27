#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.Attendance;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Attendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Attendance")]
public class PageAttendance(ILogger<PageAttendance> logger, IMediator mediator) : BaseLoginApi<SearchAttendanceArgs, PageResult<AttendanceResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Attendance", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<AttendanceResult>> InvokeAsync(SearchAttendanceArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Attendance.PageAttendance");

        if (args == null)
            return new PageResult<AttendanceResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}