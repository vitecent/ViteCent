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

namespace ViteCent.Basic.Data.BaseLogs;

/// <summary>
/// 搜索日志信息参数
/// </summary>
[Serializable]
public class SearchBaseLogsArgs : SearchArgs, IRequest<PageResult<BaseLogsResult>>
{
}