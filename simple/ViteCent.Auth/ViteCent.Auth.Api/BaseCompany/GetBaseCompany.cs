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
/// 获取公司信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseCompany")]
public class GetBaseCompany(
    ILogger<GetBaseCompany> logger,
    IMediator mediator)
    : BaseLoginApi<GetBaseCompanyArgs, DataResult<BaseCompanyResult>>
{
    /// <summary>
    /// 获取公司信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseCompany", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseCompanyResult>> InvokeAsync(GetBaseCompanyArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseCompany.GetBaseCompany");

        if (args == null)
            return new DataResult<BaseCompanyResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}