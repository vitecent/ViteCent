#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象

// 引入岗位位信息相关的数据传输对象
using ViteCent.Basic.Data.BasePost;

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器ViteCent.Basic.Api.Schedu
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
public class PageScheduleList(
    ILogger<AddSchedule> logger,
    IHttpContextAccessor httpContextAccessor,
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
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedu.PageList");

        var cancellationToken = new CancellationToken();

        var validator = new PreSearchScheduleValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new PageResult<PreAddScheduleArgs>(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        var companyId = user?.Company?.Id ?? string.Empty;
        var departmentId = user?.Department?.Id ?? string.Empty;

        var searchPositionArgs = new SearchBasePostArgs
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
                    Field = "Status",
                    Value = ((int)StatusEnum.Enable).ToString()
                }
            ]
        };

        var posts = await mediator.Send(searchPositionArgs, cancellationToken);

        if (!posts.Success)
            return new PageResult<PreAddScheduleArgs>(posts.Code, posts.Message);

        var items = new List<PreAddScheduleArgs>();

        foreach (var item in posts.Rows)
        {
            for (var date = args.StartTime; date < args.EndTime;)
            {
                var _item = new PreAddScheduleArgs
                {
                    PostId = item.Id,
                    Job = item.Name,
                    Date = date.ToString("yyyy-MM-dd")
                };

                items.Add(_item);

                date = date.AddDays(1);
            }
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
                    Field = "SceduleTimes",
                    Value = args.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Method = SearchEnum.GreaterThanOrEqual
                },
                new SearchItem
                {
                    Field = "SceduleTimes",
                    Value = args.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Method = SearchEnum.LessThanOrEqual
                }
            ]
        };

        var rows = await mediator.Send(request, cancellationToken);

        //按照岗位合并排班信息
        if (rows.Success && rows.Rows.Count > 0)
            foreach (var item in items)
            {
                var _row = rows.Rows.Where(x => x.SceduleTimes.ToString("yyyy-MM-dd") == item.Date && x.PostName == item.Job).OrderBy(x => x.Id).ToList();

                item.UserId = string.Join(",", _row.Select(x => x.UserId).Distinct().ToList());
                item.Name = string.Join(",", _row.Select(x => x.UserName).Distinct().ToList());
                item.Shift = string.Join(",", _row.Select(x => x.TypeName).Distinct().ToList());
            }

        var result = new PageResult<PreAddScheduleArgs>
        {
            Rows = items
        };

        return result;
    }
}