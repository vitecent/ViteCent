#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BasePosition;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BasePosition")]
public class GetBasePosition(ILogger<GetBasePosition> logger, IMediator mediator) : BaseLoginApi<GetBasePositionArgs, DataResult<BasePositionResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BasePosition", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BasePositionResult>> InvokeAsync(GetBasePositionArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BasePosition.GetBasePosition");

        if (args == null)
            return new DataResult<BasePositionResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}