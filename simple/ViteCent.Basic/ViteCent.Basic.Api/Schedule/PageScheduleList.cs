#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入职位信息相关的数据传输对象
using ViteCent.Auth.Data.BasePosition;

// 引入基础数据传输对象
using ViteCent.Basic.Application;

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

//引入 web 核心
using ViteCent.Core.Web;

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
/// <param name="positionInvoke"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public partial class PageScheduleList(
    ILogger<AddSchedule> logger,
    IHttpContextAccessor httpContextAccessor,
    IBaseInvoke<SearchBasePositionArgs, PageResult<BasePositionResult>> positionInvoke,
    IMediator mediator)
    : BaseApi<PreSearchScheduleArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    [Route("PageList")]
    public override async Task<BaseResult> InvokeAsync(PreSearchScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Controller.PageList");

        var cancellationToken = new CancellationToken();

        var validator = new PreSearchScheduleValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new PageResult<PreAddScheduleArgs>(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        var companyId = user?.Company?.Id ?? string.Empty;
        var departmentId = user?.Department?.Id ?? string.Empty;
        var positionId = user?.Position?.Id ?? string.Empty;

        var searchPositionArgs = new SearchBasePositionArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "CompanyId",
                    Value = companyId
                },
                new SearchItem
                {
                    Field = "DepartmentId",
                    Value = departmentId
                },
                new SearchItem
                {
                    Field = "PositionId",
                    Value = positionId
                },
                new SearchItem
                {
                    Field = "Status",
                    Value = ((int)StatusEnum.Enable).ToString()
                }
            ]
        };

        var positions = await positionInvoke.InvokePostAsync("Auth", "/BasePosition/Page", searchPositionArgs,
            user?.Token ?? string.Empty);

        if (!positions.Success)
            return new PageResult<PreAddScheduleArgs>(positions.Code, positions.Message);

        var items = new List<PreAddScheduleArgs>();

        foreach (var item in positions.Rows)
            for (var date = args.StartTime; date < args.EndTime;)
            {
                var _item = new PreAddScheduleArgs
                {
                    CompanyId = item.CompanyId,
                    DepartmentId = departmentId,
                    PositionId = item.Id,
                    UserId = item.Id,
                    Name = item.Name,
                    Shift = string.Empty,
                    Job = string.Empty,
                    Date = date.ToString("yyyy-MM-dd")
                };

                items.Add(_item);

                date = date.AddDays(1);
            }

        var request = new SearchScheduleArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "CompanyId",
                    Value = companyId
                },
                new SearchItem
                {
                    Field = "DepartmentId",
                    Value = departmentId
                },
                new SearchItem
                {
                    Field = "PositionId",
                    Value = positionId
                },
                new SearchItem
                {
                    Field = "StartTime",
                    Value = args.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Method = SearchEnum.GreaterThanOrEqual
                },
                new SearchItem
                {
                    Field = "EndTime",
                    Value = args.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Method = SearchEnum.LessThanOrEqual
                }
            ]
        };

        var rows = await mediator.Send(request, cancellationToken);

        foreach (var row in rows.Rows)
        {
            var item = items.FirstOrDefault(x =>
                x.CompanyId == row.CompanyId && x.DepartmentId == row.DepartmentId && x.UserId == row.UserId
                && x.Date == row.StartTime.ToString("yyyy-MM-dd"));

            if (item != null)
            {
                item.Name = row.UserName;
                item.Shift = row.Shift;
                item.Job = row.Job;
            }
        }

        var result = new PageResult<PreAddScheduleArgs>
        {
            Rows = items
        };

        return result;
    }
}