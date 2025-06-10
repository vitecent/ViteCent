/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入排班时间相关的数据参数
using ViteCent.Basic.Data.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion 引入命名空间

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 获取排班时间接口
/// </summary>
/// <param name="logger">日志记录器，用于记录接口的操作日志</param>
/// <param name="mediator">中介者接口，用于发送查询请求</param>
// 标记为API接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Schedule")]
public partial class GetScheduleTime(
    // 注入日志记录器
    ILogger<GetScheduleTime> logger,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<GetScheduleTimeArgs, PageResult<ScheduleTimeResult>>
{

    /// <summary>
    /// 获取排班时间
    /// </summary>
    /// <param name="args">查询参数，包含获取排班时间所需的条件</param>
    /// <returns>返回包含排班时间的数据结果对象</returns>
    // 标记为POST请求
    [HttpPost]
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    // 设置路由名称
    [Route("Time")]
    public override async Task<PageResult<ScheduleTimeResult>> InvokeAsync(GetScheduleTimeArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.GetScheduleTime");

        // 验证参数是否为空，确保请求参数的有效性
        if (args is null)
            return new PageResult<ScheduleTimeResult>(500, "参数不能为空");

        // 创建取消令牌，用于支持操作的取消
        var cancellationToken = new CancellationToken();

        // 通过中介者发送查询命令并返回结果
        return await mediator.Send(args, cancellationToken);
    }
}