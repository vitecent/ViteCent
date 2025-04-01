#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.UserLeave;

/// <summary>
/// 请假申请分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("UserLeave")]
public class PageUserLeave(ILogger<PageUserLeave> logger, IMediator mediator) : BaseLoginApi<SearchUserLeaveArgs, PageResult<UserLeaveResult>>
{
    /// <summary>
    /// 请假申请分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "UserLeave", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<UserLeaveResult>> InvokeAsync(SearchUserLeaveArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.UserLeave.PageUserLeave");

        if (args == null)
            return new PageResult<UserLeaveResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}