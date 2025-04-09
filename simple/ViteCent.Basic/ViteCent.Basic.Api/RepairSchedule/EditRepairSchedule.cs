/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.RepairSchedule;

/// <summary>
/// 编辑补卡申请接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("RepairSchedule")]
public class EditRepairSchedule(ILogger<EditRepairSchedule> logger,
    IMediator mediator) : BaseLoginApi<EditRepairScheduleArgs, BaseResult>
{
    /// <summary>
    /// 编辑补卡申请
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "RepairSchedule", "Edit" })]
    [Route("Edit")]
    public override async Task<BaseResult> InvokeAsync(EditRepairScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.RepairSchedule.EditRepairSchedule");

        var cancellationToken = new CancellationToken(true);
        var validator = new RepairScheduleValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new BaseResult(500, "部门标识不能为空");
 
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.UserId))
                return new BaseResult(500, "用户标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}