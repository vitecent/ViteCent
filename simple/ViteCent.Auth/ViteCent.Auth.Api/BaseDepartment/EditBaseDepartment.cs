/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

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
/// 编辑部门信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseDepartment")]
public class EditBaseDepartment(ILogger<EditBaseDepartment> logger,
    IMediator mediator) : BaseLoginApi<EditBaseDepartmentArgs, BaseResult>
{
    /// <summary>
    /// 编辑部门信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDepartment", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditBaseDepartmentArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDepartment.EditBaseDepartment");

        var cancellationToken = new CancellationToken(true);
        var validator = new BaseDepartmentValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}