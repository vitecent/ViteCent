/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象
using ViteCent.Auth.Application;

// 引入字典信息相关的数据传输对象
using ViteCent.Auth.Data.BaseDictionary;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDictionary;

/// <summary>
/// 新增字典信息接口
/// </summary>
/// <remarks>
/// 该接口负责处理新增字典信息的请求，主要功能包括：
/// 1. 验证用户登录状态
/// 2. 验证用户权限
/// 3. 验证新增数据的有效性
/// 4. 设置新增字典信息的请求
/// 5. 返回操作结果
/// </remarks>
/// <param name="logger">用于记录接口的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="mediator">用于发送命令请求</param>
// 标记为 API 接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseDictionary")]
public partial class AddBaseDictionary(
    // 注入日志记录器
    ILogger<AddBaseDictionary> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<AddBaseDictionaryArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 新增字典信息
    /// </summary>
    /// <remarks>
    /// 该方法主要完成以下功能：
    /// 1. 记录方法调用日志，便于追踪和调试
    /// 2. 重写调用方法
    /// 3. 创建取消令牌和数据验证器
    /// 4. 验证字典信息的有效性
    /// 5. 通过中介者发送新增命令
    /// 6. 返回操作结果
    /// </remarks>
    /// <param name="args">新增字典信息的参数</param>
    /// <returns>返回新增操作的结果</returns>
    // 标记为 POST 请求
    [HttpPost]
    // 使用权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDictionary", "Add" })]
    // 设置路由名称
    [Route("Add")]
    public override async Task<BaseResult> InvokeAsync(AddBaseDictionaryArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDictionary.AddBaseDictionary");

        // 重写调用方法
        OverrideInvoke(args, user);

        // 创建取消令牌，用于支持异步操作的取消
        var cancellationToken = new CancellationToken();

        // 创建数据验证器
        var validator = new BaseDictionaryValidator();

        // 验证字典信息的有效性
        var check = await validator.ValidateAsync(args, cancellationToken);

        // 如果验证失败，返回错误信息
        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        // 如果用户不是超级管理员，验证公司标识是否为空
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        // 通过中介者发送新增命令并返回结果
        return await mediator.Send(args, cancellationToken);
    }
}