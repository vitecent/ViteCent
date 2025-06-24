/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
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
/// 批量用户信息判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseUserList(ILogger<HasBaseUserList> logger)
    : BaseDomain<BaseUserEntity>, IRequestHandler<HasBaseUserEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// 批量用户信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseUserEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUser.HasBaseUser");

        var query = Client.Query<BaseUserEntity>();

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.Id));

        if (request.DepartmentIds.Count > 0)
            query.Where(x => request.DepartmentIds.Contains(x.DepartmentId));

        if (request.PositionIds.Count > 0)
            query.Where(x => request.PositionIds.Contains(x.PositionId));

        if (request.UserNos.Count > 0)
            query.Where(x => request.UserNos.Contains(x.UserNo));

        if (request.Usernames.Count > 0)
            query.Where(x => request.Usernames.Contains(x.Username));

        if (request.RealNames.Count > 0)
            query.Where(x => request.RealNames.Contains(x.RealName));

        if (request.IdCards.Count > 0)
            query.Where(x => request.IdCards.Contains(x.IdCard));

        if (request.Emails.Count > 0)
            query.Where(x => request.Emails.Contains(x.Email));

        if (request.Phones.Count > 0)
            query.Where(x => request.Phones.Contains(x.Phone));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "用户编号 或 登录名 或 姓名 或 身份证号 或 邮箱 或 电话 重复");

        return new BaseResult();
    }
}