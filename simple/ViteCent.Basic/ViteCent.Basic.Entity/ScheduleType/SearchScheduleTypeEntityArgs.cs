#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
public class SearchScheduleTypeEntityArgs : SearchArgs, IRequest<List<ScheduleTypeEntity>>
{
}