#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// 搜索排班信息参数
/// </summary>
[Serializable]
public class SearchScheduleArgs : SearchArgs, IRequest<PageResult<ScheduleResult>>
{
}