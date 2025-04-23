/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Auth.Entity.BaseDictionary;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseDictionary;

/// <summary>
/// 删除字典信息仓储拓展
/// </summary>
public partial class EditBaseDictionary
{
    /// <summary>
    /// 验证字典信息
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseDictionaryEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseDictionaryArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        var hasArgs = new HasBaseDictionaryEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}