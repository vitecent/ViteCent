#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 初始化接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[Route("BaseUser")]
public class Initialize(
    ILogger<Initialize> logger,
    IMediator mediator) : BaseApi<InitializeArgs, BaseResult>
{
    /// <summary>
    /// 初始化接口
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Initialize")]
    public override async Task<BaseResult> InvokeAsync(InitializeArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Initialize");

        if (string.IsNullOrEmpty(args.Username) && !string.IsNullOrWhiteSpace(args.RealName))
            args.Username = args.RealName.GetPinYin().ToCamelCase();

        if (string.IsNullOrEmpty(args.Password))
            args.Password = BaseConst.DefaultPassword;

        return await mediator.Send(args);
    }
}