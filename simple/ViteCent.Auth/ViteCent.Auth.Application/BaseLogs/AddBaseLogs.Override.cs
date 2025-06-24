/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseLogs;
using ViteCent.Auth.Entity.BaseLogs;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseLogs;

/// <summary>
/// 新增职位信息应用拓展
/// </summary>
public partial class AddBaseLogs
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="request">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator,
        AddBaseLogsListArgs request,
        BaseUserInfo user,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
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
        BaseLogsEntity entity,
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
    private async Task<BaseResult> OverrideHandle(AddBaseLogsArgs request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }
}