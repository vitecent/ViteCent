/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// 批量新增补卡申请模型
/// </summary>
[Serializable]
public class AddRepairScheduleEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 补卡申请
    /// </summary>
    public List<AddRepairScheduleEntity> Items = [];
}