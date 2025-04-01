#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUserRole;

/// <summary>
/// 用户角色分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUserRole")]
public class PageBaseUserRole(ILogger<PageBaseUserRole> logger, IMediator mediator) : BaseLoginApi<SearchBaseUserRoleArgs, PageResult<BaseUserRoleResult>>
{
    /// <summary>
    /// 用户角色分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUserRole", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseUserRoleResult>> InvokeAsync(SearchBaseUserRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUserRole.PageBaseUserRole");

        if (args == null)
            return new PageResult<BaseUserRoleResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}