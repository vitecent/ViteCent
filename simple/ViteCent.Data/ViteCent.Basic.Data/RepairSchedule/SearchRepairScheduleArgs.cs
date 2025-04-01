#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// 搜索补卡申请参数
/// </summary>
[Serializable]
public class SearchRepairScheduleArgs : SearchArgs, IRequest<PageResult<RepairScheduleResult>>
{
}