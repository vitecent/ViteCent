#region


// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象
using ViteCent.Basic.Application;

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 排班信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
// 标记为API接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Schedule")]
public class ListSchedule(
    // 注入日志记录器
    ILogger<ListSchedule> logger,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<ListScheduleArgs, PageResult<UserScheduleResult>>
{
    /// <summary>
    /// 排班信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    // 标记为POST请求
    [HttpPost]
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    [Route("List")]
    public override async Task<PageResult<UserScheduleResult>> InvokeAsync(ListScheduleArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.ListSchedule");

        if (args is null)
            return new PageResult<UserScheduleResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}