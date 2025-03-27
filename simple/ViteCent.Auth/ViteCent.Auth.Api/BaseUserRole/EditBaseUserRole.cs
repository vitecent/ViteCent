#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUserRole;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUserRole")]
public class EditBaseUserRole(ILogger<EditBaseUserRole> logger, IMediator mediator) : BaseLoginApi<EditBaseUserRoleArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUserRole", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditBaseUserRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUserRole.EditBaseUserRole");

        var cancellationToken = new CancellationToken();
        var validator = new BaseUserRoleValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        if (User.IsSuper == (int)YesNoEnum.No)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "CompanyId 不能为空");

        if (User.IsSuper == (int)YesNoEnum.No)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new BaseResult(500, "DepartmentId 不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}