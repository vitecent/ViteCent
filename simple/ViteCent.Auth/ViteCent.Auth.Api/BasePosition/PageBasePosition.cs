/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BasePosition;

/// <summary>
/// 职位信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BasePosition")]
public class PageBasePosition(
    ILogger<PageBasePosition> logger,
    IMediator mediator)
    : BaseLoginApi<SearchBasePositionArgs, PageResult<BasePositionResult>>
{
    /// <summary>
    /// 职位信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BasePosition", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BasePositionResult>> InvokeAsync(SearchBasePositionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BasePosition.PageBasePosition");

        if (args == null)
            return new PageResult<BasePositionResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}