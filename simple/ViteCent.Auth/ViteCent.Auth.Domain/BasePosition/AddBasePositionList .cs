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
/// 批量新增职位信息
/// </summary>
/// <param name="logger"></param>
public class AddBasePositionList(
    ILogger<AddBasePositionList> logger)
    : BaseDomain<AddBasePositionEntity>, IRequestHandler<AddBasePositionEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 批量新增职位信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBasePositionEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BasePosition.AddBasePositionList");

        return await base.AddAsync(request.Items);
    }
}