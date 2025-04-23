#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Basic.Application;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Controller;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="userInvoke"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class ScheduleController(
    ILogger<ScheduleController> logger,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor,
    IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke) : ControllerBase
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Add" })]
    [Route("PreAddList")]
    public async Task<BaseResult> AddList(List<PreAddScheduleArgs> args)
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

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    [Route("PageList")]
    public async Task<PageResult<PreAddScheduleArgs>> PageList(PreSearchScheduleArgs args)
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

        var searchUserArgs = new SearchBaseUserArgs
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

        var users = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Page", searchUserArgs,
            user?.Token ?? string.Empty);

        if (!users.Success)
            return new PageResult<PreAddScheduleArgs>(users.Code, users.Message);

        var items = new List<PreAddScheduleArgs>();

        foreach (var item in users.Rows)
            for (var date = args.StartTime; date < args.EndTime;)
            {
                var _item = new PreAddScheduleArgs
                {
                    CompanyId = item.CompanyId,
                    DepartmentId = item.DepartmentId,
                    PositionId = item.PositionId,
                    UserId = item.Id,
                    Name = item.RealName,
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