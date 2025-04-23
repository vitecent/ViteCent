/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseCompany;

/// <summary>
/// 公司信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseCompany")]
public class PageBaseCompany(
    ILogger<PageBaseCompany> logger,
    IMediator mediator)
    : BaseLoginApi<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>>
{
    /// <summary>
    /// 公司信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseCompany", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseCompanyResult>> InvokeAsync(SearchBaseCompanyArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseCompany.PageBaseCompany");

        if (args == null)
            return new PageResult<BaseCompanyResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}