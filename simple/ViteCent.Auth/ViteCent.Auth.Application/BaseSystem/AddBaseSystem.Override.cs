/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseSystem;

/// <summary>
/// </summary>
public partial class AddBaseSystem
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseSystemListArgs request, BaseUserInfo user)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;
        }

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();

        var companys = await mediator.CheckCompany(companyIds);

        if (!companys.Success)
            return companys;

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseSystemArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        var hasArgs = new HasBaseSystemEntityArgs
        {
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}