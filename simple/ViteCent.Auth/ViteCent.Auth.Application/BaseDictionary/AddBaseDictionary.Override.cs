/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Core;
using ViteCent.Core.Data;
using MediatR;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Auth.Entity.BaseDictionary;
using ViteCent.Core.Enums;
using ViteCent.Auth.Entity.BaseOperation;

#endregion

namespace ViteCent.Auth.Application.BaseDictionary;

/// <summary>
/// 新增字典信息仓储拓展
/// </summary>
public partial class AddBaseDictionary
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseDictionaryListArgs request, BaseUserInfo user, CancellationToken cancellationToken)
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

        var hasListArgs = new HasBaseSystemEntityListArgs
        {
            CompanyIds = [.. request.Items.Select(x => x.CompanyId).Distinct()],
            Codes = [.. request.Items.Select(x => x.Code).Distinct()],
            Names = [.. request.Items.Select(x => x.Name).Distinct()],
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
    internal static async Task OverrideTopic(IMediator mediator, TopicEnum topic, BaseDictionaryEntity entity, CancellationToken cancellationToken)
    {
        await Task.FromResult(0);
    }

    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseDictionaryArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        var hasArgs = new HasBaseDictionaryEntityArgs
        {
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}