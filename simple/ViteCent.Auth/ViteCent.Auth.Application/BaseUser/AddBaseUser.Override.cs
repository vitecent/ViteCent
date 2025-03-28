#region

using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
public partial class AddBaseUser
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddBaseUserArgs request, CancellationToken cancellationToken)
    {
        request.Status = (int)StatusEnum.Enable;
        request.Password = $"{request.Username}{request.Password}{Const.Salf}".EncryptMD5();

        var hasArgs = new HasBaseUserEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserNo = request.UserNo,
            Username = request.Username,
            RealName = request.RealName,
            IdCard = request.IdCard,
            Email = request.Email,
            Phone = request.Phone
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}