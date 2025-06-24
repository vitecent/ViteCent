/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.BaseLogs;
using ViteCent.Basic.Entity.BaseLogs;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.BaseLogs;

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
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 职位信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseLogsEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.BaseLogs.HasBaseLogs");

        var query = Client.Query<BaseLogsEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.SystemId))
            query.Where(x => x.SystemId == request.SystemId);

        if (!string.IsNullOrWhiteSpace(request.ResourceId))
            query.Where(x => x.ResourceId == request.ResourceId);

        if (!string.IsNullOrWhiteSpace(request.OperationId))
            query.Where(x => x.OperationId == request.OperationId);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "职位信息重复");

        return new BaseResult();
    }
}