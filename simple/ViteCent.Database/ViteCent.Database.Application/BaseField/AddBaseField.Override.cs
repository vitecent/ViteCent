/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入表字段信息相关的数据参数
using ViteCent.Auth.Data.BaseCompany;

// 引入核心

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入 Web 核心
using ViteCent.Core.Web;

// 引入表字段信息相关的数据参数
using ViteCent.Database.Data.BaseField;

// 引入表字段信息相关的模型
using ViteCent.Database.Entity.BaseField;

#endregion 引入命名空间

namespace ViteCent.Database.Application.BaseField;

/// <summary>
/// 新增表字段信息应用拓展
/// </summary>
public partial class AddBaseField
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
        AddBaseFieldListArgs request,
        BaseUserInfo user,
        IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
        CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;
        }

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

        var hasListArgs = new HasBaseFieldEntityListArgs
        {
            CompanyIds = companyIds,
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
        BaseFieldEntity entity,
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
    private async Task<BaseResult> OverrideHandle(AddBaseFieldArgs request,
        CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany?.Data?.Name;

        var hasArgs = new HasBaseFieldEntityArgs
        {
            CompanyId = request.CompanyId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}