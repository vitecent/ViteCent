/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDepartment;

/// <summary>
/// 获取部门信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseDepartment")]
public class GetBaseDepartment(ILogger<GetBaseDepartment> logger,
    IMediator mediator) : BaseLoginApi<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>>
{
    /// <summary>
    /// 获取部门信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDepartment", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseDepartmentResult>> InvokeAsync(GetBaseDepartmentArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDepartment.GetBaseDepartment");

        if (args == null)
            return new DataResult<BaseDepartmentResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}