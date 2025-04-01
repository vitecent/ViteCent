#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseSystem;

/// <summary>
/// 系统信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseSystem")]
public class PageBaseSystem(ILogger<PageBaseSystem> logger, IMediator mediator) : BaseLoginApi<SearchBaseSystemArgs, PageResult<BaseSystemResult>>
{
    /// <summary>
    /// 系统信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseSystem", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseSystemResult>> InvokeAsync(SearchBaseSystemArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseSystem.PageBaseSystem");

        if (args == null)
            return new PageResult<BaseSystemResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}