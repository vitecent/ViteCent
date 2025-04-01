#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 获取用户信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class GetBaseUser(ILogger<GetBaseUser> logger, IMediator mediator) : BaseLoginApi<GetBaseUserArgs, DataResult<BaseUserResult>>
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUser", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseUserResult>> InvokeAsync(GetBaseUserArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.GetBaseUser");

        if (args == null)
            return new DataResult<BaseUserResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}