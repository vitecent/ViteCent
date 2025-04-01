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