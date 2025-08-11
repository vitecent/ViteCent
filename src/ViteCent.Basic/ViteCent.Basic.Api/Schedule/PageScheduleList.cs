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
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 Api 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Schedule")]
public class PageScheduleList(
    // 注入日志记录器
    ILogger<AddSchedule> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<PreSearchScheduleArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <returns>处理结果</returns>
    [HttpPost] // 标记为POST请求
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    // 设置路由名称
    [Route("PageList")]
    public override async Task<BaseResult> InvokeAsync(PreSearchScheduleArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedu.PageList");

        // 创建取消令牌，用于支持操作的取消
        var cancellationToken = new CancellationToken();

        // 创建数据验证器
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

        // 通过中介者发送命令并返回结果
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

        // 通过中介者发送命令并返回结果
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

        // 返回操作结果
        return result;
    }
}