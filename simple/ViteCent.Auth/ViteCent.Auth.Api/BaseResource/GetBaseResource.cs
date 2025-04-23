/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseResource;

/// <summary>
/// 获取资源信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseResource")]
public class GetBaseResource(
    ILogger<GetBaseResource> logger,
    IMediator mediator)
    : BaseLoginApi<GetBaseResourceArgs, DataResult<BaseResourceResult>>
{
    /// <summary>
    /// 获取资源信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseResource", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseResourceResult>> InvokeAsync(GetBaseResourceArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseResource.GetBaseResource");

        if (args == null)
            return new DataResult<BaseResourceResult>(500, "参数不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<BaseResourceResult>(500, "公司标识不能为空");

        var check = User.CheckCompanyId(args.CompanyId);

        if (check != null && !check.Success)
            return new DataResult<BaseResourceResult>(check.Code, check.Message);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.SystemId))
                return new DataResult<BaseResourceResult>(500, "系统标识不能为空");

        return await mediator.Send(args);
    }
}