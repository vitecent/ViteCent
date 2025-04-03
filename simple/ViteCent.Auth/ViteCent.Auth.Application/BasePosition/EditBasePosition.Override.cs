/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BasePosition;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BasePosition;

/// <summary>
/// </summary>
public partial class EditBasePosition
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BasePositionEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBasePositionArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        var hasArgs = new HasBasePositionEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}