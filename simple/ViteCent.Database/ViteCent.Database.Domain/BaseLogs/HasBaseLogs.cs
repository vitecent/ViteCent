/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

using ViteCent.Database.Data.BaseLogs;
using ViteCent.Database.Entity.BaseLogs;

#endregion

namespace ViteCent.Database.Domain.BaseLogs;

/// <summary>
/// 职位信息判重领域
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseLogs(
    ILogger<HasBaseLogs> logger)
    : BaseDomain<BaseLogsEntity>, IRequestHandler<HasBaseLogsEntityArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Database";

    /// <summary>
    /// 职位信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseLogsEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Database.Domain.BaseLogs.HasBaseLogs");

        // 日志无需重复判断
        return await Task.FromResult(new BaseResult());
    }
}