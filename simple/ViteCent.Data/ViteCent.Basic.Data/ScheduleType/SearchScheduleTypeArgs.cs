#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
public class SearchScheduleTypeArgs : SearchArgs, IRequest<PageResult<ScheduleTypeResult>>
{
}