/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.ScheduleType;

/// <summary>
/// 获取基础排班接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("ScheduleType")]
public class GetScheduleType(ILogger<GetScheduleType> logger, IMediator mediator) : BaseLoginApi<GetScheduleTypeArgs, DataResult<ScheduleTypeResult>>
{
    /// <summary>
    /// 获取基础排班
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "ScheduleType", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<ScheduleTypeResult>> InvokeAsync(GetScheduleTypeArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.ScheduleType.GetScheduleType");

        if (args == null)
            return new DataResult<ScheduleTypeResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}