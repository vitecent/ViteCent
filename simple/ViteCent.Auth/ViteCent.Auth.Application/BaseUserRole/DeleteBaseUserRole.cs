/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 AutoMapper 用于对象映射
using AutoMapper;

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入用户角色相关的数据参数
using ViteCent.Auth.Data.BaseUserRole;

// 引入用户角色相关的数据模型
using ViteCent.Auth.Entity.BaseUserRole;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

#endregion 引入命名空间

namespace ViteCent.Auth.Application.BaseUserRole;

/// <summary>
/// 删除用户角色的应用服务类
/// </summary>
/// <remarks>
/// 该类负责处理用户角色的删除操作，包括：
/// 1. 验证用户角色是否存在
/// 2. 执行删除操作
/// 3. 触发相关事件通知
/// </remarks>
/// <param name="logger">日志记录器，用于记录操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于处理命令和查询</param>
public class DeleteBaseUserRole(
    // 注入日志记录器
    ILogger<DeleteBaseUserRole> logger,
    // 注入映射器接口
    IMapper mapper,
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<DeleteBaseUserRoleArgs, BaseResult>
{
    /// <summary>
    /// 处理删除用户角色的请求
    /// </summary>
    /// <param name="request">删除用户角色的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回操作结果，包含状态码和消息</returns>
    /// <remarks>
    /// 该方法的处理流程：
    /// 1. 记录操作日志
    /// 2. 将请求参数转换为查询参数
    /// 3. 查询用户角色是否存在
    /// 4. 执行删除操作
    /// 5. 触发删除事件通知
    /// </remarks>
    public async Task<BaseResult> Handle(DeleteBaseUserRoleArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUserRole.DeleteBaseUserRole");

        // 将删除请求参数映射为查询参数
        var getArgs = mapper.Map<GetBaseUserRoleEntityArgs>(request);

        // 查询要删除的用户角色模型
        var entity = await mediator.Send(getArgs, cancellationToken);

        // 如果用户角色不存在，返回错误结果
        if (entity is null)
            return new BaseResult(500, "用户角色不存在");

        // 将用户角色模型映射为删除模型参数
        var args = mapper.Map<DeleteBaseUserRoleEntity>(entity);

        // 发送删除命令并获取结果
        var result = await mediator.Send(args, cancellationToken);

        // 触发删除事件通知
        await AddBaseUserRole.OverrideTopic(mediator, TopicEnum.Delete, entity, cancellationToken);

        // 返回操作结果
        return result;
    }
}