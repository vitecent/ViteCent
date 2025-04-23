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
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseOperation;

/// <summary>
/// 批量新增操作信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseOperation")]
public class AddBaseOperationList(
    ILogger<AddBaseOperationList> logger,
    IMediator mediator)
    : BaseLoginApi<AddBaseOperationListArgs, BaseResult>
{
    /// <summary>
    /// 批量新增操作信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseOperation", "Add" })]
    [Route("AddList")]
    public override async Task<BaseResult> InvokeAsync(AddBaseOperationListArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseOperation.AddBaseOperationList");

        if (args == null)
            return new BaseResult(500, "参数不能为空");

        if (args.Items.Count == 0)
            return new BaseResult(500, "操作信息不能为空");

        var count = args.Items.Distinct().Count();

        if (count != args.Items.Count)
            return new BaseResult(500, "操作信息重复");

        var cancellationToken = new CancellationToken();
        var validator = new BaseOperationValidator();

        foreach (var item in args.Items)
        {
            AddBaseOperation.OverrideInvoke(item, User);

            var check = await validator.ValidateAsync(item, cancellationToken);

            if (!check.IsValid)
                return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.CompanyId))
                    return new BaseResult(500, "公司标识不能为空");

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.SystemId))
                    return new BaseResult(500, "系统标识不能为空");

            if (User.IsSuper != (int)YesNoEnum.Yes)
                if (string.IsNullOrEmpty(item.ResourceId))
                    return new BaseResult(500, "资源标识不能为空");

        }
        return await mediator.Send(args, cancellationToken);
    }
}