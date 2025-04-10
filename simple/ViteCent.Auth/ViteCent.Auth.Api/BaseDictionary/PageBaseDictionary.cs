/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDictionary;

/// <summary>
/// 字典信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseDictionary")]
public class PageBaseDictionary(ILogger<PageBaseDictionary> logger,
    IMediator mediator) : BaseLoginApi<SearchBaseDictionaryArgs, PageResult<BaseDictionaryResult>>
{
    /// <summary>
    /// 字典信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDictionary", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseDictionaryResult>> InvokeAsync(SearchBaseDictionaryArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDictionary.PageBaseDictionary");

        if (args == null)
            return new PageResult<BaseDictionaryResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}