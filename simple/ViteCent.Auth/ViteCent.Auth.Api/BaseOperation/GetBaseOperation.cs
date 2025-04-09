/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseOperation;

/// <summary>
/// 获取操作信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseOperation")]
public class GetBaseOperation(ILogger<GetBaseOperation> logger,
    IMediator mediator) : BaseLoginApi<GetBaseOperationArgs, DataResult<BaseOperationResult>>
{
    /// <summary>
    /// 获取操作信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseOperation", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseOperationResult>> InvokeAsync(GetBaseOperationArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseOperation.GetBaseOperation");

        if (args == null)
            return new DataResult<BaseOperationResult>(500, "参数不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<BaseOperationResult>(500, "公司标识不能为空");
 
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.SystemId))
                return new DataResult<BaseOperationResult>(500, "系统标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.ResourceId))
                return new DataResult<BaseOperationResult>(500, "资源标识不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}