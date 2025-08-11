/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using ViteCent.Basic.Data.BaseLogs;
using ViteCent.Basic.Entity.BaseLogs;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.BaseLogs;

/// <summary>
/// 删除职位信息应用拓展
/// </summary>
public partial class EditBaseLogs
{
    /// <summary>
    /// 验证职位信息
    /// </summary>
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(BaseLogsEntity entity,
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
    private async Task<BaseResult> OverrideHandle(EditBaseLogsArgs request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }
}