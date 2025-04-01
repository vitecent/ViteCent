#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// 搜索补卡申请数据参数
/// </summary>
[Serializable]
public class SearchRepairScheduleEntityArgs : SearchArgs, IRequest<List<RepairScheduleEntity>>
{
}