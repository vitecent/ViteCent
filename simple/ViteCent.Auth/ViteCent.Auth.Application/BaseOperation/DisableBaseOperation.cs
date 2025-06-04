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

// 引入操作信息相关的数据参数
using ViteCent.Auth.Data.BaseOperation;

// 引入操作信息相关的数据模型
using ViteCent.Auth.Entity.BaseOperation;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

#endregion 引入命名空间

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// 禁用操作信息的处理类
/// </summary>
/// <remarks>
/// 该类负责处理操作信息的禁用操作，包括：
/// 1. 验证用户登录状态和权限
/// 2. 验证操作信息的状态
/// 3. 更新公司状态为禁用
/// 4. 发布禁用事件通知
/// 5. 返回操作结果
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理过程中的关键信息</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者接口，用于处理命令和查询</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public partial class DisableBaseOperation(
    // 注入日志记录器
    ILogger<DisableBaseOperation> logger,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<DisableBaseOperationArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 处理禁用操作信息的请求
    /// </summary>
    /// <param name="request">禁用操作信息的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回处理结果</returns>
    /// <remarks>
    /// 该方法主要负责：
    /// 1. 记录操作日志
    /// 2. 初始化当前用户信息
    /// 3. 获取并验证公司模型信息
    /// 4. 更新公司状态为禁用
    /// 5. 发布禁用事件通知
    /// 6. 返回处理结果
    /// </remarks>
    public async Task<BaseResult> Handle(DisableBaseOperationArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseOperation.DisableBaseOperation");

        // 将请求参数映射为获取模型的参数
        var args = mapper.Map<GetBaseOperationEntityArgs>(request);

        // 通过中介者模式获取操作信息模型
        var entity = await mediator.Send(args, cancellationToken);

        // 如果操作信息不存在，返回错误信息
        if (entity is null)
            return new BaseResult(500, "操作信息不存在");

        // 如果操作信息已经是禁用状态，返回错误信息
        if (entity.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "操作信息已禁用");

        // 更新模型状态为禁用
        entity.Status = (int)StatusEnum.Disable;
        // 更新修改人信息
        entity.Updater = user?.Name ?? string.Empty;
        // 更新修改时间
        entity.UpdateTime = DateTime.Now;
        // 更新数据版本
        entity.DataVersion = DateTime.Now;

        // 通过中介者模式发送更新请求
        var result = await mediator.Send(entity, cancellationToken);

        // 发布禁用事件通知
        await AddBaseOperation.OverrideTopic(mediator, TopicEnum.Disable, entity, cancellationToken);

        // 返回处理结果
        return result;
    }
}