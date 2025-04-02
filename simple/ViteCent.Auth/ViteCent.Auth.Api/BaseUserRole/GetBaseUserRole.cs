/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUserRole;

/// <summary>
/// 获取用户角色接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUserRole")]
public class GetBaseUserRole(ILogger<GetBaseUserRole> logger,
    IMediator mediator) : BaseLoginApi<GetBaseUserRoleArgs, DataResult<BaseUserRoleResult>>
{
    /// <summary>
    /// 获取用户角色
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUserRole", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseUserRoleResult>> InvokeAsync(GetBaseUserRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUserRole.GetBaseUserRole");

        if (args == null)
            return new DataResult<BaseUserRoleResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}