/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// 搜索基础排班参数
/// </summary>
[Serializable]
public class SearchScheduleTypeArgs : SearchArgs, IRequest<PageResult<ScheduleTypeResult>>
{
}