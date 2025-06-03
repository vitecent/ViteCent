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

// 引入字典信息相关的数据结构
using ViteCent.Auth.Data.BaseDictionary;

// 引入字典信息相关的数据模型
using ViteCent.Auth.Entity.BaseDictionary;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

#endregion 引入命名空间

namespace ViteCent.Auth.Application.BaseDictionary;

/// <summary>
/// 启用字典信息处理器
/// </summary>
/// <remarks>
/// 该类负责处理启用字典信息的请求，主要功能包括：
/// 1. 验证字典信息是否存在
/// 2. 检查公司当前状态
/// 3. 更新公司状态为启用
/// 4. 记录更新信息
/// 5. 发送启用事件通知
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于结构和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送命令和查询</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public partial class EnableBaseDictionary(
    // 注入日志记录器
    ILogger<EnableBaseDictionary> logger,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<EnableBaseDictionaryArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 处理启用字典信息的请求
    /// </summary>
    /// <remarks>
    /// 该方法实现了IRequestHandler接口的Handle方法，主要功能包括：
    /// 1. 记录方法调用日志
    /// 2. 初始化当前用户信息
    /// 3. 查询并验证字典信息
    /// 4. 检查公司状态是否可以启用
    /// 5. 更新公司状态和相关信息
    /// 6. 发送启用事件通知
    /// </remarks>
    /// <param name="request">启用字典信息的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回操作结果</returns>
    public async Task<BaseResult> Handle(EnableBaseDictionaryArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseDictionary.EnableBaseDictionary");

        // 初始化当前用户信息
        user = httpContextAccessor.InitUser();

        // 将请求参数转换为模型查询参数
        var args = mapper.Map<GetBaseDictionaryEntityArgs>(request);

        // 查询字典信息
        var entity = await mediator.Send(args, cancellationToken);

        // 验证字典信息是否存在
        if (entity is null)
            return new BaseResult(500, "字典信息不存在");

        // 检查字典信息是否已经启用
        if (entity.Status == (int)StatusEnum.Enable)
            return new BaseResult(500, "字典信息已启用");

        // 更新公司状态为启用
        entity.Status = (int)StatusEnum.Enable;
        // 设置更新人信息
        entity.Updater = user?.Name ?? string.Empty;
        // 设置更新时间
        entity.UpdateTime = DateTime.Now;
        // 更新数据版本
        entity.DataVersion = DateTime.Now;

        // 保存更新后的字典信息
        var result = await mediator.Send(entity, cancellationToken);

        // 发送启用事件通知
        await AddBaseDictionary.OverrideTopic(mediator, TopicEnum.Enable, entity, cancellationToken);

        // 返回操作结果
        return result;
    }
}