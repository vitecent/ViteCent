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

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Http;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入基础排班相关的数据结构
using ViteCent.Basic.Data.ScheduleType;

// 引入基础排班相关的数据模型
using ViteCent.Basic.Entity.ScheduleType;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// 基础排班分页查询处理器
/// </summary>
/// <remarks>
/// 该类负责处理基础排班的分页查询请求，主要功能包括：
/// 1. 接收并处理分页查询参数
/// 2. 执行基础排班的分页查询
/// 3. 转换查询结果为响应格式
/// 4. 返回分页查询结果
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于结构和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
public partial class PageScheduleType(
    // 注入日志记录器
    ILogger<PageScheduleType> logger,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<SearchScheduleTypeArgs, PageResult<ScheduleTypeResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    // <summary>
    /// 处理基础排班分页查询请求
    /// </summary>
    /// <remarks>
    /// 该方法实现了IRequestHandler接口的Handle方法，主要功能包括：
    /// 1. 记录方法调用日志
    /// 2. 将请求参数转换为模型查询参数
    /// 3. 执行分页查询操作
    /// 4. 转换查询结果为响应结构
    /// 5. 构造并返回分页结果
    /// </remarks>
    /// <param name="request">分页查询请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回分页查询结果，包含基础排班列表</returns>
    public async Task<PageResult<ScheduleTypeResult>> Handle(SearchScheduleTypeArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Application.ScheduleType.PageScheduleType");

        // 获取用户信息
        user = httpContextAccessor.InitUser();

        // 将请求参数转换为模型查询参数
        var args = mapper.Map<SearchScheduleTypeEntityArgs>(request);

        // 调用重写方法
        OverrideHandle(args, user);

        // 通过中介者发送查询请求，获取查询结果
        var list = await mediator.Send(args, cancellationToken);

        // 将查询结果转换为响应结构列表
        var rows = mapper.Map<List<ScheduleTypeResult>>(list);

        // 构造分页结果对象，包含分页信息和数据列表
        var result = new PageResult<ScheduleTypeResult>(args.Offset, args.Limit, args.Total, rows);

        // 返回分页查询结果
        return result;
    }
}