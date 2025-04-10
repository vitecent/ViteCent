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
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDictionary;

/// <summary>
/// 获取字典信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseDictionary")]
public class GetBaseDictionary(ILogger<GetBaseDictionary> logger,
    IMediator mediator) : BaseLoginApi<GetBaseDictionaryArgs, DataResult<BaseDictionaryResult>>
{
    /// <summary>
    /// 获取字典信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDictionary", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseDictionaryResult>> InvokeAsync(GetBaseDictionaryArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDictionary.GetBaseDictionary");

        if (args == null)
            return new DataResult<BaseDictionaryResult>(500, "参数不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<BaseDictionaryResult>(500, "公司标识不能为空");

        var check = User.CheckCompanyId(args.CompanyId);

        if (check != null && !check.Success)
            return new DataResult<BaseDictionaryResult>(check.Code, check.Message);

        return await mediator.Send(args, new CancellationToken());
    }
}