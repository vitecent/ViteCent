/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入数据库信息相关的数据参数

// 引入数据库信息相关的数据参数 引入核心数据类型
using ViteCent.Core.Data;
using ViteCent.Database.Data.BaseDatabase;

// 引入数据库信息相关的数据模型
using ViteCent.Database.Entity.BaseDatabase;

#endregion 引入命名空间

namespace ViteCent.Database.Application.BaseDatabase;

/// <summary>
/// 删除数据库信息应用拓展
/// </summary>
public partial class EditBaseDatabase
{
    /// <summary>
    /// 验证数据库信息
    /// </summary>
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(BaseDatabaseEntity entity,
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
    private async Task<BaseResult> OverrideHandle(EditBaseDatabaseArgs request,
        CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany?.Data?.Name;

        var hasArgs = new HasBaseDatabaseEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}