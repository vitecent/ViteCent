/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseOperation;

/// <summary>
/// 编辑操作信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseOperation")]
public class EditBaseOperation(
    ILogger<EditBaseOperation> logger,
    IMediator mediator)
    : BaseLoginApi<EditBaseOperationArgs, BaseResult>
{
    /// <summary>
    /// 编辑操作信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseOperation", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditBaseOperationArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseOperation.EditBaseOperation");

        var cancellationToken = new CancellationToken();
        var validator = new BaseOperationValidator(true);

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        var checkCompany = User.CheckCompanyId(args.CompanyId);

        if (checkCompany != null && !checkCompany.Success)
            return checkCompany;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.SystemId))
                return new BaseResult(500, "系统标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.ResourceId))
                return new BaseResult(500, "资源标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}