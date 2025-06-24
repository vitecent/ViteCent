/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseResource;

/// <summary>
/// </summary>
public partial class AddBaseResource
{
    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="request">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseResourceListArgs request,
        BaseUserInfo user, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        foreach (var item in request.Items)
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var systemIds = request.Items.Select(x => x.SystemId).Distinct().ToList();

        var companys = await mediator.CheckCompanys(companyIds);

        if (!companys.Success)
            return companys;

        foreach (var item in companys.Rows)
        {
            var items = request.Items.Where(x => x.CompanyId == item.Id).ToList();

            foreach (var data in items)
                data.CompanyName = item.Name;
        }

        var systems = await mediator.CheckSystems(companyIds, systemIds);

        if (!systems.Success)
            return systems;

        foreach (var item in systems.Rows)
        {
            var items = request.Items.Where(x => x.SystemId == item.Id).ToList();

            foreach (var data in items)
                data.SystemName = item.Name;
        }

        var hasListArgs = new HasBaseResourceEntityListArgs
        {
            CompanyIds = companyIds,
            SystemIds = systemIds,
            Codes = [.. request.Items.Select(x => x.Code).Distinct()],
            Names = [.. request.Items.Select(x => x.Name).Distinct()]
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="topic">话题</param>
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task OverrideTopic(IMediator mediator, TopicEnum topic, BaseResourceEntity entity,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(AddBaseResourceArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany?.Data?.Name;

        var hasSystem = await mediator.CheckSystem(request.CompanyId, request.SystemId);

        if (!hasSystem.Success)
            return hasSystem;

        request.SystemName = hasSystem?.Data?.Name;

        var hasArgs = new HasBaseResourceEntityArgs
        {
            CompanyId = request.CompanyId,
            SystemId = request.SystemId,
            Code = request.Code,
            Name = request.Name
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}