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
public class GetUserInfo(
    ILogger<GetUserInfo> logger,
    IMediator mediator) : BaseLoginApi<GetUserInfoArgs, DataResult<BaseUserInfo>>
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("GetUserInfo")]
    public override async Task<DataResult<BaseUserInfo>> InvokeAsync(GetUserInfoArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.GetUserInfo");

        return await mediator.Send(args);
    }
}