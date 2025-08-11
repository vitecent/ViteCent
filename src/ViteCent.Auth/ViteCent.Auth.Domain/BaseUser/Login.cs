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
    /// <param name="request">�������</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>������</returns>
    public async Task<BaseUserEntity> Handle(LoginEntityArgs request, CancellationToken cancellationToken)
    {
        return await base.GetAsync(x => x.Username == request.Username && x.Password == request.Password);
    }
}