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
public class GetAttendance(ILogger<GetAttendance> logger, IMediator mediator) : BaseLoginApi<GetAttendanceArgs, DataResult<AttendanceResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Attendance", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<AttendanceResult>> InvokeAsync(GetAttendanceArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Attendance.GetAttendance");

        if (args == null)
            return new DataResult<AttendanceResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}