/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseLogs;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseLogs;

/// <summary>
/// 日志信息分页领域
/// </summary>
/// <param name="logger"></param>
public class PageBaseLogs(ILogger<PageBaseLogs> logger) : BaseDomain<BaseLogsEntity>, IRequestHandler<SearchBaseLogsEntityArgs, List<BaseLogsEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 日志信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseLogsEntity>> Handle(SearchBaseLogsEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseLogs.PageBaseLogs");

        return await base.PageAsync(request);
    }
}