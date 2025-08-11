/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 Asp.Net Core Mvc 核心功能
using AutoMapper;
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
/// 生成代码接口
/// </summary>
/// <remarks>该接口负责处理生成代码的请求</remarks>
/// <param name="logger">日志记录器，用于记录接口的操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
[ApiController] // 标记为 Api 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Builder")]
public class BuildCode(
    // 注入日志记录器
    ILogger<BuildCode> logger,
    IMapper mapper)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<Setting, BaseResult>
{
    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="args">查询参数，包含生成代码所需的条件</param>
    /// <returns>返回包含设置的数据结果对象</returns>
    [HttpPost] // 标记为 Post 请求
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Builder", "Build" })]
    // 设置路由名称
    [Route("Build")]
    public override async Task<BaseResult> InvokeAsync(Setting args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Builder.Api.Build.BuildCode");

        // 获取数据库信息
        await mapper.GetDatabase(args);

        // 生成代码
        await args.BuildCode();

        // 返回结果
        return await Task.FromResult(new BaseResult());
    }
}