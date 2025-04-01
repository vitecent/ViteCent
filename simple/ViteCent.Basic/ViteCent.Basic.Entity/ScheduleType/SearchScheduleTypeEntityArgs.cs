#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// 搜索基础排班数据参数
/// </summary>
[Serializable]
public class SearchScheduleTypeEntityArgs : SearchArgs, IRequest<List<ScheduleTypeEntity>>
{
}