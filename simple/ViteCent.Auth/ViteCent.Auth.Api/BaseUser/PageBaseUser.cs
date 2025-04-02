/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 用户信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class PageBaseUser(ILogger<PageBaseUser> logger,
    IMediator mediator) : BaseLoginApi<SearchBaseUserArgs, PageResult<BaseUserResult>>
{
    /// <summary>
    /// 用户信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUser", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseUserResult>> InvokeAsync(SearchBaseUserArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.PageBaseUser");

        if (args == null)
            return new PageResult<BaseUserResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}