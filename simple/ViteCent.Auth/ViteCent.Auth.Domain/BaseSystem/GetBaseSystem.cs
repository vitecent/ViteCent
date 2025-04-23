/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseSystem;

/// <summary>
/// 获取系统信息
/// </summary>
/// <param name="logger"></param>
public class GetBaseSystem(
    ILogger<GetBaseSystem> logger)
    : BaseDomain<BaseSystemEntity>, IRequestHandler<GetBaseSystemEntityArgs, BaseSystemEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 获取系统信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseSystemEntity> Handle(GetBaseSystemEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseSystem.GetBaseSystem");

        var query = Client.Query<BaseSystemEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        return await query.FirstAsync(cancellationToken);
    }
}