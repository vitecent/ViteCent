#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 上班接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class FirstSchedule(
    ILogger<FirstSchedule> logger,
    IMediator mediator) : BaseLoginApi<FirstScheduleArgs, BaseResult>
{
    /// <summary>
    /// 上班
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Edit" })]
    [Route("First")]
    public override async Task<BaseResult> InvokeAsync(FirstScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.FirstSchedule");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = User.Company.Id;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = User.Department.Id;

        var cancellationToken = new CancellationToken();
        var validator = new FirstScheduleValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        var checkCompany = User.CheckCompanyId(args.CompanyId);

        if (checkCompany != null && !checkCompany.Success)
            return checkCompany;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new BaseResult(500, "部门标识不能为空");

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.UserId))
                return new BaseResult(500, "用户标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}