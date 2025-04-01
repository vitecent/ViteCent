#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// </summary>
[Serializable]
public class SearchRepairScheduleEntityArgs : SearchArgs, IRequest<List<RepairScheduleEntity>>
{
}