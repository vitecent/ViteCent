#region

using MediatR;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUser;

/// <summary>
/// </summary>
public class Login : BaseDomain<BaseUserEntity>, IRequestHandler<LoginEntityArgs, BaseUserEntity>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseUserEntity> Handle(LoginEntityArgs request, CancellationToken cancellationToken)
    {
        return await base.GetAsync(x => x.Username == request.Username && x.Password == request.Password);
    }
}