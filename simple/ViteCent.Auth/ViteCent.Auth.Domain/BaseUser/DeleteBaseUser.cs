/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUser;

/// <summary>
/// 删除用户信息领域
/// </summary>
/// <param name="logger"></param>
public class DeleteBaseUser(
    ILogger<DeleteBaseUser> logger)
    : BaseDomain<DeleteBaseUserEntity>, IRequestHandler<DeleteBaseUserEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 删除用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseUserEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUser.DeleteBaseUser");

        return await DeleteAsync(request);
    }
}