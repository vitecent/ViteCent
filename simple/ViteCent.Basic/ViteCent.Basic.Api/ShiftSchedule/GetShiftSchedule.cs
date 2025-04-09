/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.ShiftSchedule;

/// <summary>
/// 获取换班申请接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("ShiftSchedule")]
public class GetShiftSchedule(ILogger<GetShiftSchedule> logger,
    IMediator mediator) : BaseLoginApi<GetShiftScheduleArgs, DataResult<ShiftScheduleResult>>
{
    /// <summary>
    /// 获取换班申请
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "ShiftSchedule", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<ShiftScheduleResult>> InvokeAsync(GetShiftScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.ShiftSchedule.GetShiftSchedule");

        if (args == null)
            return new DataResult<ShiftScheduleResult>(500, "参数不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<ShiftScheduleResult>(500, "公司标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new DataResult<ShiftScheduleResult>(500, "部门标识不能为空");
 
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.UserId))
                return new DataResult<ShiftScheduleResult>(500, "用户标识不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}