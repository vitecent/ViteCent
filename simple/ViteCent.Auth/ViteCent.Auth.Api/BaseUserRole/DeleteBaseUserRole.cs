/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
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
/// 删除用户角色接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUserRole")]
public class DeleteBaseUserRole(ILogger<DeleteBaseUserRole> logger, IMediator mediator) : BaseLoginApi<DeleteBaseUserRoleArgs, BaseResult>
{
    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUserRole", "Delete" })]
    [Route("Delete")]
    public override async Task<BaseResult> InvokeAsync(DeleteBaseUserRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUserRole.DeleteBaseUserRole");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        return await mediator.Send(args);
    }
}