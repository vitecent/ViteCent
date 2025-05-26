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
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserRest;

/// <summary>
/// 批量新增调休申请领域
/// </summary>
/// <param name="logger"></param>
public class AddUserRestList(
    ILogger<AddUserRestList> logger)
    : BaseDomain<AddUserRestEntity>, IRequestHandler<AddUserRestEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 批量新增调休申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddUserRestEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserRest.AddUserRestList");

        return await base.AddAsync(request.Items);
    }
}