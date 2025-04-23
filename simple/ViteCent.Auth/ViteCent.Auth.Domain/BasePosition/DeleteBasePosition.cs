/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BasePosition;

/// <summary>
/// 删除职位信息
/// </summary>
/// <param name="logger"></param>
public class DeleteBasePosition(
    ILogger<DeleteBasePosition> logger)
    : BaseDomain<DeleteBasePositionEntity>, IRequestHandler<DeleteBasePositionEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 删除职位信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBasePositionEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BasePosition.DeleteBasePosition");

        return await DeleteAsync(request);
    }
}