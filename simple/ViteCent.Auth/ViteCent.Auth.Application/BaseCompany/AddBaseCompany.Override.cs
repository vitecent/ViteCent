#region

using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// </summary>
public partial class AddBaseCompany
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        request.Status = (int)StatusEnum.Enable;

        var hasArgs = new HasBaseCompanyEntityArgs
        {
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}