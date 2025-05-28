#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;

// 引入基础数据传输对象
using ViteCent.Basic.Application;

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入排班模型
using ViteCent.Basic.Entity.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;
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
/// <param name="userInvoke"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class PreAddScheduleList(
    ILogger<AddSchedule> logger,
    IHttpContextAccessor httpContextAccessor,
    IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke,
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
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedu.PreAddList");

        var cancellationToken = new CancellationToken();

        var items = new List<AddScheduleArgs>();

        //获取所有用户
        var searchUserArgs = new SearchBaseUserArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        var users = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Page", searchUserArgs, user?.Token ?? string.Empty);

        if (!users.Success)
            return new BaseResult(users.Code, users.Message);

        foreach (var item in args)
        {
            //按照,拆分用户 第一个为主班，后边均为副班
            var names = item.Name.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Distinct().ToList();

            var firt = true;

            foreach (var name in names)
            {
                var user = users.Rows.FirstOrDefault(x => x.RealName == name);

                if (user == null)
                    return new BaseResult(500, $"用户{name}不存在");

                var _item = new AddScheduleArgs
                {
                    CompanyId = user.CompanyId,
                    DepartmentId = user.DepartmentId,
                    UserId = user.Id,
                    UserName = user.RealName,
                    Shift = "副班",
                    Job = item.Job,
                    StartTime = DateTime.Parse($"{item.Date} 00:00:00"),
                    EndTime = DateTime.Parse($"{item.Date} 23:59:59"),
                    Status = (int)ScheduleEnum.None
                };

                if (firt)
                    _item.Shift = "主班";

                items.Add(_item);

                firt = false;
            }
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