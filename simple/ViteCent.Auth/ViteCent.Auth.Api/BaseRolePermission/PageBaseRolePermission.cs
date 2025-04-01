#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseRolePermission;

/// <summary>
/// 角色权限分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRolePermission")]
public class PageBaseRolePermission(ILogger<PageBaseRolePermission> logger, IMediator mediator) : BaseLoginApi<SearchBaseRolePermissionArgs, PageResult<BaseRolePermissionResult>>
{
    /// <summary>
    /// 角色权限分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRolePermission", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseRolePermissionResult>> InvokeAsync(SearchBaseRolePermissionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRolePermission.PageBaseRolePermission");

        if (args == null)
            return new PageResult<BaseRolePermissionResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}