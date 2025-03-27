#region

using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseDepartment;

/// <summary>
/// </summary>
public partial class AddBaseDepartment
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddBaseDepartmentArgs request, CancellationToken cancellationToken)
    {
        request.Status = (int)StatusEnum.Enable;

        var hasArgs = new HasBaseDepartmentEntityArgs
        {
            CompanyId = request.CompanyId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}