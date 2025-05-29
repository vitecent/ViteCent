#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Application;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 重置密码接口
/// </summary>
/// <param name="logger"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class ResetPasword(
    ILogger<ResetPasword> logger,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator) : BaseApi<ResetPaswordArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUser", "ResetPasword" })]
    [Route("ResetPasword")]
    public override async Task<BaseResult> InvokeAsync(ResetPaswordArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.ResetPasword");

        var cancellationToken = new CancellationToken();
        var validator = new ResetPaswordValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        return await mediator.Send(args);
    }
}