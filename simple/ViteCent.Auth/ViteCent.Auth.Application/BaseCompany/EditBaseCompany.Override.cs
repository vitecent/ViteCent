#region

using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// </summary>
public partial class EditBaseCompany
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBaseCompanyEntityArgs
        {
            Id = request.Id,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}