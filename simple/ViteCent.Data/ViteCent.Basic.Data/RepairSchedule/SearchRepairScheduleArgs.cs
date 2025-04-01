#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// </summary>
[Serializable]
public class SearchRepairScheduleArgs : SearchArgs, IRequest<PageResult<RepairScheduleResult>>
{
}