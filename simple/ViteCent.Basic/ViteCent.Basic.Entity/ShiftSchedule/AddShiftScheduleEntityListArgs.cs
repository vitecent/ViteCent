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

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 批量新增换班申请模型
/// </summary>
[Serializable]
public class AddShiftScheduleEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 换班申请
    /// </summary>
    public List<AddShiftScheduleEntity> Items = [];
}