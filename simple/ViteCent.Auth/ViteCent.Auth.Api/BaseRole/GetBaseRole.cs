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
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseRole;

/// <summary>
/// 获取角色信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRole")]
public class GetBaseRole(
    ILogger<GetBaseRole> logger,
    IMediator mediator)
    : BaseLoginApi<GetBaseRoleArgs, DataResult<BaseRoleResult>>
{
    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRole", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseRoleResult>> InvokeAsync(GetBaseRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRole.GetBaseRole");

        if (args == null)
            return new DataResult<BaseRoleResult>(500, "参数不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<BaseRoleResult>(500, "公司标识不能为空");

        var check = User.CheckCompanyId(args.CompanyId);

        if (check != null && !check.Success)
            return new DataResult<BaseRoleResult>(check.Code, check.Message);

        return await mediator.Send(args);
    }
}