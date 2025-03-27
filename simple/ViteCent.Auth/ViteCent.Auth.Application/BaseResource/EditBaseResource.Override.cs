#region

using ViteCent.Auth.Data.BaseResource;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseResource;

/// <summary>
/// </summary>
public partial class EditBaseResource
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditBaseResourceArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBaseResourceEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            SystemId = request.SystemId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}