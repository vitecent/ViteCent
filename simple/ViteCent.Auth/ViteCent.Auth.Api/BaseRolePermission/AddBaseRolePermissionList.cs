/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
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
/// 批量新增角色权限接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRolePermission")]
public class AddBaseRolePermissionList(
    ILogger<AddBaseRolePermissionList> logger,
    IMediator mediator)
    : BaseLoginApi<AddBaseRolePermissionListArgs, BaseResult>
{
    /// <summary>
    /// 批量新增角色权限
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRolePermission", "Add" })]
    [Route("AddList")]
    public override async Task<BaseResult> InvokeAsync(AddBaseRolePermissionListArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRolePermission.AddBaseRolePermissionList");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        if (args.Items.Count == 0)
            return new BaseResult(500, "角色权限不能为空");

        var count = args.Items.Distinct().Count();

        if (count != args.Items.Count)
            return new BaseResult(500, "角色权限重复");

        var cancellationToken = new CancellationToken();
        var validator = new BaseRolePermissionValidator();

        foreach (var item in args.Items)
        {
            AddBaseRolePermission.OverrideInvoke(item, User);

            var check = await validator.ValidateAsync(item, cancellationToken);

            if (!check.IsValid)
                return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.CompanyId))
                    return new BaseResult(500, "公司标识不能为空");

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.RoleId))
                    return new BaseResult(500, "角色标识不能为空");

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.SystemId))
                    return new BaseResult(500, "系统标识不能为空");

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.ResourceId))
                    return new BaseResult(500, "资源标识不能为空");

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.OperationId))
                    return new BaseResult(500, "操作标识不能为空");

        }
        return await mediator.Send(args, cancellationToken);
    }
}