#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
[Serializable]
public class SearchScheduleArgs : SearchArgs, IRequest<PageResult<ScheduleResult>>
{
}