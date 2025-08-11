#region

// 引入 MediatR 用于实现中介者模式

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象

// 引入基础日志数据传输对象
using ViteCent.Auth.Data.BaseLogs;

// 引入用户信息相关的数据传输对象
using ViteCent.Auth.Data.BaseUser;

// 引入核心
using ViteCent.Core;
using ViteCent.Core.Cache;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 退出登录接口
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
[ApiController] // 标记为 API 接口
// 设置路由前缀
[Route("BaseUser")]
public class Loginout(
    // 注入日志记录器
    ILogger<Loginout> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入缓存器
    IBaseCache cache)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<LoginoutArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <returns>退出登录结果</returns>
    [HttpPost] // 标记为POST请求
    // 设置路由名称
    [Route("Loginout")]
    public override async Task<BaseResult> InvokeAsync(LoginoutArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Loginout");

        if (!string.IsNullOrWhiteSpace(user?.Id))
        {
            // 创建日志参数对象，用于记录操作日志
            var logsArgs = new AddBaseLogsArgs()
            {
                CompanyId = user?.Company?.Id ?? string.Empty,
                CompanyName = user?.Company?.Name ?? string.Empty,
                DepartmentId = user?.Department?.Id ?? string.Empty,
                DepartmentName = user?.Department?.Name ?? string.Empty,
                SystemId = string.Empty,
                SystemName = "Auth",
                ResourceId = string.Empty,
                ResourceName = "BaseUser",
                OperationId = string.Empty,
                OperationName = "Loginout",
                Description = "退出登录",
                Args = args.ToJson()
            };

            cache.DeleteKey($"User{user.Id}");
            cache.DeleteKey($"UserInfo{user?.Id}");
        }

        return await Task.FromResult(new BaseResult());
    }
}