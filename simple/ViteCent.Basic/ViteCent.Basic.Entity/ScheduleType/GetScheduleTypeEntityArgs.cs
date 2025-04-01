#region

using MediatR;

#endregion

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// 获取基础排班数据参数
/// </summary>
[Serializable]
public class GetScheduleTypeEntityArgs : IRequest<ScheduleTypeEntity>
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
}