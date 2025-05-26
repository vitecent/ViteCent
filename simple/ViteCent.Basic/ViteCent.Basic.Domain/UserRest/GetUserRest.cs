/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserRest;

/// <summary>
/// 获取调休申请领域
/// </summary>
/// <param name="logger"></param>
public class GetUserRest(
    ILogger<GetUserRest> logger)
    : BaseDomain<UserRestEntity>, IRequestHandler<GetUserRestEntityArgs, UserRestEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 获取调休申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UserRestEntity> Handle(GetUserRestEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserRest.GetUserRest");

        var query = Client.Query<UserRestEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        return await query.FirstAsync(cancellationToken);
    }
}