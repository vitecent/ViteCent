/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 AutoMapper 用于对象映射
using AutoMapper;

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入排班信息相关的数据参数
using ViteCent.Basic.Data.Schedule;

// 引入排班信息相关的数据模型
using ViteCent.Basic.Entity.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 排班信息分页查询处理器
/// </summary>
/// <remarks>
/// 该类负责处理排班信息的分页查询请求，主要功能包括：
/// 1. 接收并处理分页查询参数
/// 2. 执行排班信息的分页查询
/// 3. 转换查询结果为响应格式
/// 4. 返回分页查询结果
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
public class PageSchedule(
    // 注入日志记录器
    ILogger<PageSchedule> logger,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<SearchScheduleArgs, PageResult<ScheduleResult>>
{
    // <summary>
    /// 处理排班信息分页查询请求
    /// </summary>
    /// <remarks>
    /// 该方法实现了IRequestHandler接口的Handle方法，主要功能包括：
    /// 1. 记录方法调用日志
    /// 2. 将请求参数转换为模型查询参数
    /// 3. 执行分页查询操作
    /// 4. 转换查询结果为响应参数
    /// 5. 构造并返回分页结果
    /// </remarks>
    /// <param name="request">分页查询请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回分页查询结果，包含排班信息列表</returns>
    public async Task<PageResult<ScheduleResult>> Handle(SearchScheduleArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.PageSchedule");

        // 将请求参数转换为模型查询参数
        var args = mapper.Map<SearchScheduleEntityArgs>(request);

        // 通过中介者发送查询请求，获取查询结果
        var list = await mediator.Send(args, cancellationToken);

        // 将查询结果转换为响应参数列表
        var rows = mapper.Map<List<ScheduleResult>>(list);

        // 构造分页结果对象，包含分页信息和数据列表
        var result = new PageResult<ScheduleResult>(args.Offset, args.Limit, args.Total, rows);

        // 返回分页查询结果
        return result;
    }
}