/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入设置相关的数据参数
using ViteCent.Builder.Data.Build;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion 引入命名空间

namespace ViteCent.Builder.Api.Build;

/// <summary>
/// 获取设置接口
/// </summary>
/// <remarks>该接口负责处理获取设置的请求</remarks>
/// <param name="logger">日志记录器，用于记录接口的操作日志</param>
// 标记为API接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Builder")]
public class GetBuildSeeting(
    // 注入日志记录器
    ILogger<GetBuildSeeting> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<BaseArgs, DataResult<Setting>>
{
    /// <summary>
    /// 获取设置
    /// </summary>
    /// <param name="args">查询参数，包含获取设置所需的条件</param>
    /// <returns>返回包含设置的数据结果对象</returns>
    // 标记为POST请求
    [HttpPost]
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Builder", "Setting" })]
    // 设置路由名称
    [Route("Setting")]
    public override async Task<DataResult<Setting>> InvokeAsync(BaseArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Builder.Api.Build.GetBuildSeeting");

        // 返回结果
        return await Task.FromResult(new DataResult<Setting>(new Setting()));
    }
}