/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseRole;

/// <summary>
/// 角色信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRole")]
public class PageBaseRole(
    ILogger<PageBaseRole> logger,
    IMediator mediator)
    : BaseLoginApi<SearchBaseRoleArgs, PageResult<BaseRoleResult>>
{
    /// <summary>
    /// 角色信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRole", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseRoleResult>> InvokeAsync(SearchBaseRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRole.PageBaseRole");

        if (args == null)
            return new PageResult<BaseRoleResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}