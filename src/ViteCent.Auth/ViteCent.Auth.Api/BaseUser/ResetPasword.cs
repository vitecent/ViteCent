#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 重置密码接口
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 API 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseUser")]
public class ResetPasword(
    // 注入日志记录器
    ILogger<ResetPasword> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入缓存器
    IBaseCache cache,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<ResetPaswordArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="args">重置密码参数</param>
    /// <returns>处理结果</returns>
    [HttpPost] // 标记为POST请求
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUser", "ResetPasword" })]
    // 设置路由名称
    [Route("ResetPasword")]
    public override async Task<BaseResult> InvokeAsync(ResetPaswordArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.ResetPasword");

        // 创建取消令牌，用于支持操作的取消
        var cancellationToken = new CancellationToken();

        // 创建数据验证器
        var validator = new ResetPaswordValidator();

        // 验证重置密码参数的有效性
        var check = await validator.ValidateAsync(args, cancellationToken);

        // 如果验证失败，返回错误信息
        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(companyId))
            companyId = args.CompanyId;

        var request = new GetBaseUserEntityArgs
        {
            Id = args.UserId,
            CompanyId = companyId
        };

        var entity = await mediator.Send(request, cancellationToken);

        if (entity is null)
            return new DataResult<LoginResult>(500, "用户不存在");

        args.Password = $"{entity.Username}{args.Password}{BaseConst.Salf}".EncryptMD5();

        entity.Password = args.Password;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.Version = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        cache.DeleteKey($"User{user?.Id}");
        cache.DeleteKey($"UserInfo{user?.Id}");

        return result;
    }
}