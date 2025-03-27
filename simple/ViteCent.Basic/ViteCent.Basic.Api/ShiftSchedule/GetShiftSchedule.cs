#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.ShiftSchedule;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("ShiftSchedule")]
public class GetShiftSchedule(ILogger<GetShiftSchedule> logger, IMediator mediator) : BaseLoginApi<GetShiftScheduleArgs, DataResult<ShiftScheduleResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "ShiftSchedule", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<ShiftScheduleResult>> InvokeAsync(GetShiftScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.ShiftSchedule.GetShiftSchedule");

        if (args == null)
            return new DataResult<ShiftScheduleResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}