#region

using ViteCent.Auth.Data.BasePosition;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BasePosition;

/// <summary>
/// </summary>
public partial class AddBasePosition
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBasePositionArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBasePositionEntityArgs
        {
            CompanyId = request.CompanyId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}