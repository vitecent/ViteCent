#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 删除排班信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class DeleteSchedule(ILogger<DeleteSchedule> logger, IMediator mediator) : BaseLoginApi<DeleteScheduleArgs, BaseResult>
{
    /// <summary>
    /// 删除排班信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Delete" })]
    [Route("Delete")]
    public override async Task<BaseResult> InvokeAsync(DeleteScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.DeleteSchedule");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        return await mediator.Send(args);
    }
}