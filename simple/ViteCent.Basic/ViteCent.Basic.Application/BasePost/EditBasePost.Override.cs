/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using ViteCent.Basic.Data.BasePost;
using ViteCent.Basic.Entity.BasePost;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.BasePost;

/// <summary>
/// 删除职位信息应用拓展
/// </summary>
public partial class EditBasePost
{
    /// <summary>
    /// 验证职位信息
    /// </summary>
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(BasePostEntity entity,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(EditBasePostArgs request,
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
            Id = request.Id,
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}