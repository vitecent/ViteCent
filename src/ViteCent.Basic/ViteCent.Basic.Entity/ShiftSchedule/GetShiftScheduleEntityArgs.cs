/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

#endregion 引入命名空间

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 获取换班申请模型参数
/// </summary>
[Serializable]
public class GetShiftScheduleEntityArgs : IRequest<ShiftScheduleEntity>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}