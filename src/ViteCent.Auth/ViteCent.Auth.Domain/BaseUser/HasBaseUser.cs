/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseUser(ILogger<HasBaseUser> logger)
    : BaseDomain<BaseUserEntity>, IRequestHandler<HasBaseUserEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseUserEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUser.HasBaseUser");

        var query = Client.Query<BaseUserEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.PositionId))
            query.Where(x => x.PositionId == request.PositionId);

        if (!string.IsNullOrWhiteSpace(request.UserNo))
            query.Where(x => x.UserNo == request.UserNo);

        if (!string.IsNullOrWhiteSpace(request.Username))
            query.Where(x => x.Username == request.Username);

        if (!string.IsNullOrWhiteSpace(request.RealName))
            query.Where(x => x.RealName == request.RealName);

        if (!string.IsNullOrWhiteSpace(request.IdCard))
            query.Where(x => x.IdCard == request.IdCard);

        if (!string.IsNullOrWhiteSpace(request.Email))
            query.Where(x => x.Email == request.Email);

        if (!string.IsNullOrWhiteSpace(request.Phone))
            query.Where(x => x.Phone == request.Phone);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "用户编号 或 登录名 或 姓名 或 身份证号 或 邮箱 或 电话 重复");

        return new BaseResult();
    }
}