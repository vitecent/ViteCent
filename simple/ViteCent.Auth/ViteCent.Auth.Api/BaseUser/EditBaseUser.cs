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
/// 编辑用户信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class EditBaseUser(ILogger<EditBaseUser> logger, IMediator mediator) : BaseLoginApi<EditBaseUserArgs, BaseResult>
{
    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUser", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditBaseUserArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.EditBaseUser");

        var cancellationToken = new CancellationToken();
        var validator = new BaseUserValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "CompanyId 不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new BaseResult(500, "DepartmentId 不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.PositionId))
                return new BaseResult(500, "PositionId 不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}