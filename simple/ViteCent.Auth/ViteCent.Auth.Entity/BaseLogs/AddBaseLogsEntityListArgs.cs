/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseLogs;

/// <summary>
/// 批量新增日志信息模型
/// </summary>
[Serializable]
public class AddBaseLogsEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public List<AddBaseLogsEntity> Items = [];
}