#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象
using ViteCent.Basic.Application;

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入排班模型
using ViteCent.Basic.Entity.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public partial class PreAddScheduleList(
    ILogger<AddSchedule> logger,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator)
    : BaseListApi<List<PreAddScheduleArgs>, BaseResult>
{
    /// <summary>
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Add" })]
    [Route("PreAddList")]
    public override async Task<BaseResult> InvokeAsync(List<PreAddScheduleArgs> args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Controller.PreAddList");

        var cancellationToken = new CancellationToken();

        var items = new List<AddScheduleArgs>();

        foreach (var item in args)
        {
            var _item = new AddScheduleArgs
            {
                CompanyId = item.CompanyId,
                DepartmentId = item.DepartmentId,
                UserId = item.UserId,
                Shift = item.Shift,
                Job = item.Job,
                StartTime = DateTime.Parse($"{item.Date} 00:00:00"),
                EndTime = DateTime.Parse($"{item.Date} 23:59:59"),
                Status = (int)ScheduleEnum.None
            };

            items.Add(_item);
        }

        var request = new AddScheduleListArgs
        {
            Items = items
        };

        var companyId = user?.Company?.Id ?? string.Empty;
        var departmentId = user?.Department?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

            if (string.IsNullOrWhiteSpace(item.DepartmentId))
                item.DepartmentId = departmentId;
        }

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var departmentIds = request.Items.Select(x => x.DepartmentId).Distinct().ToList();
        var userIds = request.Items.Select(x => x.UserId).Distinct().ToList();

        var deleteScheduleArgs = new DeleteScheduleEntityListArgs
        {
            CompanyIds = companyIds,
            DepartmentIds = departmentIds,
            UserIds = userIds,
            StartTime = request.Items.Min(x => x.StartTime),
            EndTime = request.Items.Max(x => x.EndTime)
        };

        var delete = await mediator.Send(deleteScheduleArgs, cancellationToken);

        if (!delete.Success)
            return delete;

        return await mediator.Send(request, cancellationToken);
    }
}