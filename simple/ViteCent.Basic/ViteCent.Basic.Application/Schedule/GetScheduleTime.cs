#region 引入命名空间

// 引入 AutoMapper 用于对象映射
using AutoMapper;

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Http;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入排班时间相关的数据模型
using ViteCent.Auth.Data.BaseDictionary;

// 引入排班时间相关的数据参数
using ViteCent.Basic.Data.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入 web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 获取排班时间处理器
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="dictionaryInvoke">字典信息访问对象</param>
public class GetScheduleTime(
    // 注入日志记录器
    ILogger<GetSchedule> logger,
    // 注入对象映射器
    IMapper mapper,
    // 注入中介者
    IMediator mediator,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入字典访问器
    IBaseInvoke<SearchBaseDictionaryArgs, PageResult<BaseDictionaryResult>> dictionaryInvoke)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<GetScheduleTimeArgs, PageResult<ScheduleTimeResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 处理获取排班时间的请求
    /// </summary>
    /// <param name="request">获取排班时间的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回包含排班时间的数据结果对象</returns>
    public async Task<PageResult<ScheduleTimeResult>> Handle(GetScheduleTimeArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.GetScheduleTime");

        //获取当前时段
        var dictionarys = await dictionaryInvoke.GetDictionary(user?.Company?.Id ?? string.Empty, "Times", user?.Token ?? string.Empty);

        if (!dictionarys.Success)
            return new PageResult<ScheduleTimeResult>(dictionarys.Code, dictionarys.Message);

        var dictionary = dictionarys.Rows.OrderByDescending(x => x.CreateTime).FirstOrDefault();

        if (dictionary is null)
            return new PageResult<ScheduleTimeResult>(500, "字典不存在");

        var times = new List<ScheduleTimeResult>(5)
        {
            new(){ Times = "15:00-18:00" },
            new(){ Times = "18:00-22:30" },
            new(){ Times = "22:30-08:30" }
        };

        if (dictionary.Value == ((int)TimelEnum.Summer).ToString())
        {
            times.Insert(0, new ScheduleTimeResult() { Times = "08:30-11:45" });
            times.Insert(1, new ScheduleTimeResult() { Times = "11:45-15:00" });
        }
        else
        {
            times.Insert(0, new ScheduleTimeResult() { Times = "09:00-12:15" });
            times.Insert(1, new ScheduleTimeResult() { Times = "12:15-15:00" });
        }

        // 返回结果
        return new PageResult<ScheduleTimeResult>(0, 0, 0, times);
    }
}