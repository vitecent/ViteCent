#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.UserLeave;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("UserLeave")]
public class AddUserLeave(ILogger<AddUserLeave> logger, IMediator mediator) : BaseLoginApi<AddUserLeaveArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "UserLeave", "Add" })]
    [Route("Add")]
    public override async Task<BaseResult> InvokeAsync(AddUserLeaveArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.UserLeave.AddUserLeave");

        var cancellationToken = new CancellationToken();
        var validator = new UserLeaveValidator();
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