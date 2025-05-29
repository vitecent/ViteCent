/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using ViteCent.Auth.Data.BaseLogs;
using ViteCent.Auth.Entity.BaseLogs;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseLogs;

/// <summary>
/// 删除职位信息应用拓展
/// </summary>
public partial class EditBaseLogs
{
    /// <summary>
    /// 验证职位信息
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseLogsEntity entity,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseLogsArgs request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }
}