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
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseRole;

/// <summary>
/// 新增角色信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRole")]
public partial class AddBaseRole(ILogger<AddBaseRole> logger,
    IMediator mediator) : BaseLoginApi<AddBaseRoleArgs, BaseResult>
{
    /// <summary>
    /// 新增角色信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRole", "Add" })]
    [Route("Add")]
    public override async Task<BaseResult> InvokeAsync(AddBaseRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRole.AddBaseRole");

        OverrideInvoke(args, User);

        var cancellationToken = new CancellationToken();
        var validator = new BaseRoleValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}