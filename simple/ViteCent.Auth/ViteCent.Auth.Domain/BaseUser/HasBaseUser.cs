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
/// <param name="logger"></param>
public class HasBaseUser(ILogger<HasBaseUser> logger) : BaseDomain<BaseUserEntity>, IRequestHandler<HasBaseUserEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(HasBaseUserEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUser.HasBaseUser");

        var query = Client.Query<BaseUserEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

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
            return new BaseResult(500, "用户编号或登录名或姓名或身份证号或邮箱或电话重复");

        return new BaseResult(string.Empty);
    }
}