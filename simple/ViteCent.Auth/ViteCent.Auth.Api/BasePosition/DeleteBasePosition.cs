#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BasePosition;

/// <summary>
/// 删除职位信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BasePosition")]
public class DeleteBasePosition(ILogger<DeleteBasePosition> logger, IMediator mediator) : BaseLoginApi<DeleteBasePositionArgs, BaseResult>
{
    /// <summary>
    /// 删除职位信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BasePosition", "Delete" })]
    [Route("Delete")]
    public override async Task<BaseResult> InvokeAsync(DeleteBasePositionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BasePosition.DeleteBasePosition");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        return await mediator.Send(args);
    }
}