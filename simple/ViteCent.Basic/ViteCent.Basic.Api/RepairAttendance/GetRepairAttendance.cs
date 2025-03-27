#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.RepairAttendance;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.RepairAttendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("RepairAttendance")]
public class GetRepairAttendance(ILogger<GetRepairAttendance> logger, IMediator mediator) : BaseLoginApi<GetRepairAttendanceArgs, DataResult<RepairAttendanceResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "RepairAttendance", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<RepairAttendanceResult>> InvokeAsync(GetRepairAttendanceArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.RepairAttendance.GetRepairAttendance");

        if (args == null)
            return new DataResult<RepairAttendanceResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}