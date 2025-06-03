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

// 引入角色权限相关的数据结构
using ViteCent.Auth.Data.BaseRolePermission;

// 引入角色权限相关的数据模型
using ViteCent.Auth.Entity.BaseRolePermission;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

#endregion 引入命名空间

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// 禁用角色权限的处理类
/// </summary>
/// <remarks>
/// 该类负责处理角色权限的禁用操作，包括：
/// 1. 验证用户登录状态和权限
/// 2. 验证角色权限的状态
/// 3. 更新公司状态为禁用
/// 4. 发布禁用事件通知
/// 5. 返回操作结果
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理过程中的关键信息</param>
/// <param name="mapper">对象映射器，用于结构和模型对象之间的转换</param>
/// <param name="mediator">中介者接口，用于处理命令和查询</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public partial class DisableBaseRolePermission(
    // 注入日志记录器
    ILogger<DisableBaseRolePermission> logger,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<DisableBaseRolePermissionArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 处理禁用角色权限的请求
    /// </summary>
    /// <param name="request">禁用角色权限的请求参数</param>
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
    public async Task<BaseResult> Handle(DisableBaseRolePermissionArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseRolePermission.DisableBaseRolePermission");

        // 初始化当前用户信息
        user = httpContextAccessor.InitUser();

        // 将请求参数映射为获取模型的参数
        var args = mapper.Map<GetBaseRolePermissionEntityArgs>(request);

        // 通过中介者模式获取角色权限模型
        var entity = await mediator.Send(args, cancellationToken);

        // 如果角色权限不存在，返回错误信息
        if (entity is null)
            return new BaseResult(500, "角色权限不存在");

        // 如果角色权限已经是禁用状态，返回错误信息
        if (entity.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "角色权限已禁用");

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
        await AddBaseRolePermission.OverrideTopic(mediator, TopicEnum.Disable, entity, cancellationToken);

        // 返回处理结果
        return result;
    }
}