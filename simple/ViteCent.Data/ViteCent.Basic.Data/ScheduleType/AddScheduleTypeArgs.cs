/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// 新增基础排班参数
/// </summary>
[Serializable]
public class AddScheduleTypeArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 简介
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否跨天
    /// </summary>
    public int Overnight { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int ScheduleType { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; } = string.Empty;
}