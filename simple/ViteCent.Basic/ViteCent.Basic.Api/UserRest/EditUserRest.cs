/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.UserRest;

/// <summary>
/// 编辑调休申请接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("UserRest")]
public class EditUserRest(ILogger<EditUserRest> logger,
    IMediator mediator) : BaseLoginApi<EditUserRestArgs, BaseResult>
{
    /// <summary>
    /// 编辑调休申请
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "UserRest", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditUserRestArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.UserRest.EditUserRest");

        var cancellationToken = new CancellationToken();
        var validator = new UserRestValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new BaseResult(500, "部门标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}