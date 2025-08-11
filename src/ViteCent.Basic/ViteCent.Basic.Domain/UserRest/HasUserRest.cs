#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserRest;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasUserRest(ILogger<HasUserRest> logger)
    : BaseDomain<UserRestEntity>, IRequestHandler<HasUserRestEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasUserRestEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserRest.HasUserRest");

        var query = Client.Query<UserRestEntity>().Where(x => x.Status != (int)UserRestEnum.Pass);

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        query.Where(x => (x.StartTime >= request.StartTime && x.StartTime <= request.EndTime) ||
                         (x.EndTime >= request.StartTime && x.EndTime <= request.EndTime) ||
                         (x.StartTime <= request.StartTime && x.EndTime >= request.EndTime));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "调休重复");

        return new BaseResult();
    }
}