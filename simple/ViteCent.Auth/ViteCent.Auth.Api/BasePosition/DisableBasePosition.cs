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
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BasePosition;

/// <summary>
/// 禁用职位信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BasePosition")]
public class DisableBasePosition(
    ILogger<DisableBasePosition> logger,
    IMediator mediator)
    : BaseLoginApi<DisableBasePositionArgs, BaseResult>
{
    /// <summary>
    /// 禁用职位信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BasePosition", "Disable" })]
    [Route("Disable")]
    public override async Task<BaseResult> InvokeAsync(DisableBasePositionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BasePosition.DisableBasePosition");

        var cancellationToken = new CancellationToken();

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        var check = User.CheckCompanyId(args.CompanyId);

        if (check != null && !check.Success)
            return check;

        return await mediator.Send(args, cancellationToken);
    }
}