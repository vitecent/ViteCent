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

// 引入日志信息相关的数据模型
using ViteCent.Auth.Entity.BaseLogs;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Auth.Domain.BaseLogs;

/// <summary>
/// 新增日志信息领域服务类
/// </summary>
/// <remarks>该类负责处理单个日志信息的新增操作 继承自 BaseDomain 基类并实现 IRequestHandler 接口 通过依赖注入方式接收日志记录器，用于记录操作日志</remarks>
/// <param name="logger">日志记录器实例</param>
public class AddBaseLogs(
    // 注入日志记录器
    ILogger<AddBaseLogs> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseDomain<AddBaseLogsEntity>, IRequestHandler<AddBaseLogsEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// 处理新增日志信息的请求
    /// </summary>
    /// <remarks>实现 IRequestHandler 接口的 Handle 方法 记录操作日志并调用基类的 AddAsync 方法执行新增操作</remarks>
    /// <param name="request">包含要新增的日志信息的请求模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回新增操作的结果</returns>
    public async Task<BaseResult> Handle(AddBaseLogsEntity request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseLogs.AddBaseLogs");

        try
        {
            // 执行分表新增操作
            await Client.Insert(request).SplitTable().ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            // 返回操作失败结果，包含错误信息
            return new BaseResult(500, e.Message);
        }

        //返回操作成功结果
        return new BaseResult();
    }
}