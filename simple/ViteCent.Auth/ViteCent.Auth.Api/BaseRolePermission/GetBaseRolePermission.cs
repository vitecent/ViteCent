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
/// 获取角色权限接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRolePermission")]
public class GetBaseRolePermission(ILogger<GetBaseRolePermission> logger,
    IMediator mediator) : BaseLoginApi<GetBaseRolePermissionArgs, DataResult<BaseRolePermissionResult>>
{
    /// <summary>
    /// 获取角色权限
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRolePermission", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseRolePermissionResult>> InvokeAsync(GetBaseRolePermissionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRolePermission.GetBaseRolePermission");

        if (args == null)
            return new DataResult<BaseRolePermissionResult>(500, "参数不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<BaseRolePermissionResult>(500, "公司标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.RoleId))
                return new DataResult<BaseRolePermissionResult>(500, "角色标识不能为空");
 
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.SystemId))
                return new DataResult<BaseRolePermissionResult>(500, "系统标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.ResourceId))
                return new DataResult<BaseRolePermissionResult>(500, "资源标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.OperationId))
                return new DataResult<BaseRolePermissionResult>(500, "操作标识不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}