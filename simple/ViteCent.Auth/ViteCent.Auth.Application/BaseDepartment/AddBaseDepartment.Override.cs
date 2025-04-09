/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Entity.BaseUser;
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
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseDepartmentListArgs request, BaseUserInfo user, CancellationToken cancellationToken)
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

        foreach (var item in companys.Rows)
        {
            var items = request.Items.Where(x => x.CompanyId == item.Id).ToList();

            foreach (var data in items)
                data.CompanyName = item.Name;
        }

        var hasListArgs = new HasBaseDepartmentEntityListArgs
        {
            CompanyIds = [.. request.Items.Select(x => x.CompanyId).Distinct()],
            Codes = [.. request.Items.Select(x => x.Code).Distinct()],
            Names = [.. request.Items.Select(x => x.Name).Distinct()],
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseDepartmentArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany.Data.Name;

        if (!string.IsNullOrWhiteSpace(request.ParentId))
        {
            var hasParentArgs = new GetBaseDepartmentEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.ParentId,
            };

            var hasParent = await mediator.Send(hasParentArgs, cancellationToken);

            if (hasParent == null)
                return new BaseResult(500, "父级部门不存在");

            if (hasParent.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "父级部门已禁用");

            if (string.IsNullOrWhiteSpace(hasParent.Level))
                request.Level = hasParent.Id;
            else
                request.Level = $"{hasParent.Level},{hasParent.Id}";
        }

        var hasArgs = new HasBaseDepartmentEntityArgs
        {
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}