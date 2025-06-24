/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Basic.Data.BasePost;
using ViteCent.Basic.Entity.BasePost;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.BasePost;

/// <summary>
/// 新增职位信息应用拓展
/// </summary>
public partial class AddBasePost
{
    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="request">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <param name="companyInvoke">公司信息访问对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator,
        AddBasePostListArgs request,
        BaseUserInfo user,
        IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
        CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        foreach (var item in request.Items)
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();

        var companys = await companyInvoke.CheckCompanys(companyIds, user?.Token ?? string.Empty);

        if (!companys.Success)
            return companys;

        foreach (var item in companys.Rows)
        {
            var items = request.Items.Where(x => x.CompanyId == item.Id).ToList();

            foreach (var data in items)
                data.CompanyName = item.Name;
        }

        var hasListArgs = new HasBasePostEntityListArgs
        {
            CompanyIds = companyIds,
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
    internal static async Task OverrideTopic(IMediator mediator,
        TopicEnum topic,
        BasePostEntity entity,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(AddBasePostArgs request,
        CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany?.Data?.Name;

        var hasArgs = new HasBasePostEntityArgs
        {
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}