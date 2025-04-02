/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseSystem;

/// <summary>
/// 编辑系统信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseSystem")]
public class EditBaseSystem(ILogger<EditBaseSystem> logger, IMediator mediator) : BaseLoginApi<EditBaseSystemArgs, BaseResult>
{
    /// <summary>
    /// 编辑系统信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseSystem", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditBaseSystemArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseSystem.EditBaseSystem");

        var cancellationToken = new CancellationToken();
        var validator = new BaseSystemValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}