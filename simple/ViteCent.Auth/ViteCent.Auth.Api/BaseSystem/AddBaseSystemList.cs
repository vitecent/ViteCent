/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseSystem;

/// <summary>
/// 批量新增系统信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseSystem")]
public class AddBaseSystemList(ILogger<AddBaseSystemList> logger,
    IMediator mediator) : BaseLoginApi<AddBaseSystemListArgs, BaseResult>
{
    /// <summary>
    /// 批量新增系统信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseSystem", "Add" })]
    [Route("AddList")]
    public override async Task<BaseResult> InvokeAsync(AddBaseSystemListArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseSystem.AddBaseSystemList");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        if (args.Items.Count == 0)
            return new BaseResult(500, "数据不能为空");

        var count = args.Items.Distinct().Count();

        if (count != args.Items.Count)
            return new BaseResult(500, "数据重复");

        var cancellationToken = new CancellationToken();
        var validator = new BaseSystemValidator();

        foreach (var item in args.Items)
        {
            AddBaseSystem.OverrideInvoke(item, User);

            var result = await validator.ValidateAsync(item, cancellationToken);

            if (!result.IsValid)
                return new BaseResult(500, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.CompanyId))
                    return new BaseResult(500, "公司标识不能为空");
        }

        return await mediator.Send(args, cancellationToken);
    }
}