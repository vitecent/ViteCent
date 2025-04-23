/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
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
/// 基础排班分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("ScheduleType")]
public class PageScheduleType(
    ILogger<PageScheduleType> logger,
    IMediator mediator)
    : BaseLoginApi<SearchScheduleTypeArgs, PageResult<ScheduleTypeResult>>
{
    /// <summary>
    /// 基础排班分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "ScheduleType", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<ScheduleTypeResult>> InvokeAsync(SearchScheduleTypeArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.ScheduleType.PageScheduleType");

        if (args == null)
            return new PageResult<ScheduleTypeResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}