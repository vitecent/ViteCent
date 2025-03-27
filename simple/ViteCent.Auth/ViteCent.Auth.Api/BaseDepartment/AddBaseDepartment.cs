#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDepartment;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseDepartment")]
public class AddBaseDepartment(ILogger<AddBaseDepartment> logger, IMediator mediator) : BaseLoginApi<AddBaseDepartmentArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDepartment", "Add" })]
    [Route("Add")]
    public override async Task<BaseResult> InvokeAsync(AddBaseDepartmentArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDepartment.AddBaseDepartment");

        var cancellationToken = new CancellationToken();
        var validator = new BaseDepartmentValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        if (User.IsSuper == (int)YesNoEnum.No)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "CompanyId 不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}