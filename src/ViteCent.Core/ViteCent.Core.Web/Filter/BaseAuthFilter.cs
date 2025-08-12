#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

using System.Security.Claims;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Web.Filter;

/// <summary>
/// 权限验证过滤器
/// </summary>
/// <remarks>用于验证用户对特定系统资源的访问权限 检查用户登录状态、Token有效性以及操作权限</remarks>
/// <param name="cache">缓存器，用于存储和验证用户Token</param>
/// <param name="configuration">配置接口，用于获取JWT配置信息</param>
/// <param name="system">系统标识</param>
/// <param name="resource">资源标识</param>
/// <param name="operation">操作标识</param>
[AttributeUsage(AttributeTargets.Method)]
public class BaseAuthFilter(
    IBaseCache cache,
    IConfiguration configuration,
    string system,
    string resource,
    string operation) : ActionFilterAttribute
{
    /// <summary>
    /// 操作标识
    /// </summary>
    /// <remarks>标识用户要执行的具体操作</remarks>
    public string Operation { get; set; } = operation;

    /// <summary>
    /// 资源标识
    /// </summary>
    /// <remarks>标识要访问的系统资源</remarks>
    public string Resource { get; set; } = resource;

    /// <summary>
    /// 系统标识
    /// </summary>
    /// <remarks>标识当前操作所属的系统</remarks>
    public string System { get; set; } = system;

    /// <summary>
    /// 执行操作前的权限验证
    /// </summary>
    /// <param name="context">操作执行上下文，包含HTTP请求和用户信息</param>
    /// <remarks>
    /// 1. 验证用户Token的存在性和有效性
    /// 2. 检查用户登录状态
    /// 3. 验证用户是否具有执行特定操作的权限
    /// 4. 更新Token和用户信息的过期时间
    /// </remarks>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var logger = new BaseLogger(typeof(BaseAuthFilter));

        var result = new JsonResult(new BaseResult(301, "登录超时,请重新登录"));

        var httpContext = context.HttpContext;

        var token = httpContext.Request.Headers[BaseConst.Token];

        if (string.IsNullOrWhiteSpace(token))
        {
            logger.LogInformation($"InvokeAsync {System}:{Resource}:{Operation} Not Token");
            context.Result = result;
            return;
        }

        var json = httpContext.User.FindFirstValue(ClaimTypes.UserData);

        if (string.IsNullOrWhiteSpace(json))
        {
            logger.LogInformation($"InvokeAsync {System}:{Resource}:{Operation} Not UserData");
            context.Result = result;

            return;
        }

        var user = json.DeJson<BaseUserInfo>();

        if (user is null)
        {
            logger.LogInformation($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} Not Login");
            context.Result = result;

            return;
        }

        if (user?.IsSuper != (int)YesNoEnum.Yes)
            if (!IsAUth(user, System, Resource, Operation))
            {
                logger.LogInformation($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} No Permission");
                context.Result = new JsonResult(new BaseResult(401, "您没有权限访问该资源"));

                return;
            }

        logger.LogInformation($"{user?.Name} InvokeAsync {System}:{Resource}:{Operation} Success");
    }

    /// <summary>
    /// 验证用户权限
    /// </summary>
    /// <param name="user">用户信息</param>
    /// <param name="system">系统标识</param>
    /// <param name="resource">资源标识</param>
    /// <param name="operation">操作标识</param>
    /// <returns>true标识有权限，false标识无权限</returns>
    /// <remarks>从缓存中获取用户的权限信息，验证用户是否具有指定的系统、资源和操作权限</remarks>
    private bool IsAUth(BaseUserInfo? user, string system, string resource, string operation)
    {
        var auth = new List<BaseSystemInfo>();

        if (cache.HasKey($"UserInfo{user?.Id}"))
            auth = cache.GetString<List<BaseSystemInfo>>($"UserInfo{user?.Id}");

        if (auth is null) return false;

        var _system = auth?.FirstOrDefault(x => x.Code == system);

        if (_system is null) return false;

        var _resource = _system.Resources.FirstOrDefault(x => x.Code == resource);

        if (_resource is null) return false;

        var _operation = _resource.Operations.FirstOrDefault(x => x.Code == operation);

        if (_operation is not null) return true;

        return false;
    }
}