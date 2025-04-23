/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseRolePermission;

/// <summary>
/// 编辑角色权限接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRolePermission")]
public class EditBaseRolePermission(
    ILogger<EditBaseRolePermission> logger,
    IMediator mediator)
    : BaseLoginApi<EditBaseRolePermissionArgs, BaseResult>
{
    /// <summary>
    /// 编辑角色权限
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRolePermission", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditBaseRolePermissionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRolePermission.EditBaseRolePermission");

        var cancellationToken = new CancellationToken();
        var validator = new BaseRolePermissionValidator(true);

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
            if (string.IsNullOrEmpty(args.RoleId))
                return new BaseResult(500, "角色标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.SystemId))
                return new BaseResult(500, "系统标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.ResourceId))
                return new BaseResult(500, "资源标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.OperationId))
                return new BaseResult(500, "操作标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}