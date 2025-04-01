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
/// 删除请假申请接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("UserLeave")]
public class DeleteUserLeave(ILogger<DeleteUserLeave> logger, IMediator mediator) : BaseLoginApi<DeleteUserLeaveArgs, BaseResult>
{
    /// <summary>
    /// 删除请假申请
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "UserLeave", "Delete" })]
    [Route("Delete")]
    public override async Task<BaseResult> InvokeAsync(DeleteUserLeaveArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.UserLeave.DeleteUserLeave");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        return await mediator.Send(args);
    }
}