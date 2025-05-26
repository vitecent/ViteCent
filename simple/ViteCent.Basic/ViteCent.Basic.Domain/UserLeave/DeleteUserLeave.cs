/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserLeave;

/// <summary>
/// 删除请假申请领域
/// </summary>
/// <param name="logger"></param>
public class DeleteUserLeave(
    ILogger<DeleteUserLeave> logger)
    : BaseDomain<DeleteUserLeaveEntity>, IRequestHandler<DeleteUserLeaveEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 删除请假申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteUserLeaveEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserLeave.DeleteUserLeave");

        return await DeleteAsync(request);
    }
}