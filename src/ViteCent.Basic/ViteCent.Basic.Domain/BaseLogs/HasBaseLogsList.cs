/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.BaseLogs;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.BaseLogs;

/// <summary>
/// 批量职位信息判重领域
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseLogsList(
    ILogger<HasBaseLogsList> logger)
    : BaseDomain<BaseLogsEntity>, IRequestHandler<HasBaseLogsEntityListArgs, BaseResult>
{
    /// <summary>
    /// 职位信息库名称
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 批量职位信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseLogsEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.BaseLogs.HasBaseLogs");

        // 日志无需重复判断
        return await Task.FromResult(new BaseResult());
    }
}