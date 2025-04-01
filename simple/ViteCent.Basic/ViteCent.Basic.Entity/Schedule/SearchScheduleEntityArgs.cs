#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// 搜索排班信息数据参数
/// </summary>
[Serializable]
public class SearchScheduleEntityArgs : SearchArgs, IRequest<List<ScheduleEntity>>
{
}