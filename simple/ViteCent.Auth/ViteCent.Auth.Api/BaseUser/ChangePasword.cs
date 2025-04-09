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
/// 重置密码接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class ChangePasword(ILogger<ChangePasword> logger,
    IMediator mediator) : BaseLoginApi<ChangePaswordArgs, BaseResult>
{
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("ChangePasword")]
    public override async Task<BaseResult> InvokeAsync(ChangePaswordArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.ChangePasword");

        var cancellationToken = new CancellationToken();
        var validator = new ChangePaswordArgsValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new DataResult<ChangePasword>(500, result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        return await mediator.Send(args);
    }
}