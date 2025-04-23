#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 重置密码接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class ResetPasword(
    ILogger<ResetPasword> logger,
    IMediator mediator) : BaseLoginApi<ResetPaswordArgs, BaseResult>
{
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
        var validator = new ResetPaswordArgsValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        return await mediator.Send(args);
    }
}