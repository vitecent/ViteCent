/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入调休申请相关的数据模型
using ViteCent.Basic.Entity.UserRest;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Basic.Domain.UserRest;

/// <summary>
/// 新增调休申请领域服务类
/// </summary>
/// <remarks>
/// 该类负责处理单个调休申请的新增操作
/// 继承自 BaseDomain 基类并实现 IRequestHandler 接口
/// 通过依赖注入方式接收日志记录器，用于记录操作日志
/// </remarks>
/// <param name="logger">日志记录器实例</param>
public class AddUserRest(
    // 注入日志记录器
    ILogger<AddUserRest> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseDomain<AddUserRestEntity>, IRequestHandler<AddUserRestEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 处理新增调休申请的请求
    /// </summary>
    /// <remarks>
    /// 实现 IRequestHandler 接口的 Handle 方法
    /// 记录操作日志并调用基类的 AddAsync 方法执行新增操作
    /// </remarks>
    /// <param name="request">包含要新增的调休申请的请求模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回新增操作的结果</returns>
    public async Task<BaseResult> Handle(AddUserRestEntity request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserRest.AddUserRest");

        // 调用基类的新增方法执行实际的新增操作
        return await base.AddAsync(request);
    }
}