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
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseCompany;

/// <summary>
/// 新增公司信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseCompany")]
public partial class AddBaseCompany(ILogger<AddBaseCompany> logger,
    IMediator mediator) : BaseLoginApi<AddBaseCompanyArgs, BaseResult>
{
    /// <summary>
    /// 新增公司信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseCompany", "Add" })]
    [Route("Add")]
    public override async Task<BaseResult> InvokeAsync(AddBaseCompanyArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseCompany.AddBaseCompany");

        OverrideInvoke(args, User);

        var cancellationToken = new CancellationToken();
        var validator = new BaseCompanyValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        return await mediator.Send(args, cancellationToken);
    }
}