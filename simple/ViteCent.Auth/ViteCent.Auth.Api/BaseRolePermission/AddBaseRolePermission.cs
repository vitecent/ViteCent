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
/// 新增角色权限接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRolePermission")]
public partial class AddBaseRolePermission(ILogger<AddBaseRolePermission> logger, IMediator mediator) : BaseLoginApi<AddBaseRolePermissionArgs, BaseResult>
{
    /// <summary>
    /// 新增角色权限
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRolePermission", "Add" })]
    [Route("Add")]
    public override async Task<BaseResult> InvokeAsync(AddBaseRolePermissionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRolePermission.AddBaseRolePermission");

        OverrideInvoke(args);

        var cancellationToken = new CancellationToken();
        var validator = new BaseRolePermissionValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "CompanyId 不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}