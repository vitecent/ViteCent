/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Entity.BaseCompany;
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
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseCompanyListArgs request,
        BaseUserInfo user, CancellationToken cancellationToken)
    {
        var companyIds = request.Items.Select(x => x.ParentId).Distinct().ToList();

        if (companyIds.Count > 0)
        {
            var companys = await mediator.CheckCompany(companyIds);

            if (!companys.Success)
                return companys;
        }

        var hasListArgs = new HasBaseCompanyEntityListArgs
        {
            Codes = [.. request.Items.Select(x => x.Code).Distinct()],
            Names = [.. request.Items.Select(x => x.Name).Distinct()]
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="topic"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task OverrideTopic(IMediator mediator, TopicEnum topic, BaseCompanyEntity entity,
        CancellationToken cancellationToken)
    {
        await Task.FromResult(0);
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId))
        {
            var hasParentArgs = new GetBaseCompanyEntityArgs
            {
                Id = request.ParentId
            };

            var hasParent = await mediator.Send(hasParentArgs, cancellationToken);

            if (hasParent == null)
                return new BaseResult(500, "父级公司不存在");

            if (hasParent.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "父级公司已禁用");

            if (string.IsNullOrWhiteSpace(hasParent.Level))
                request.Level = hasParent.Id;
            else
                request.Level = $"{hasParent.Level},{hasParent.Id}";
        }

        var hasArgs = new HasBaseCompanyEntityArgs
        {
            Code = request.Code,
            Name = request.Name
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}