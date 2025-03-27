#region

using MediatR;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// </summary>
[Serializable]
public class SearchScheduleEntityArgs : SearchArgs, IRequest<List<ScheduleEntity>>
{
}