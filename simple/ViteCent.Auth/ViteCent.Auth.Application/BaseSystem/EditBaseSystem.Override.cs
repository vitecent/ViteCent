#region

using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseSystem;

/// <summary>
/// </summary>
public partial class EditBaseSystem
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditBaseSystemArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBaseSystemEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}