/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// 新增排班信息参数
/// </summary>
[Serializable]
public class AddScheduleArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 上班时间
    /// </summary>
    public DateTime? FirstTime { get; set; }

    /// <summary>
    /// 下班时间
    /// </summary>
    public DateTime? LastTime { get; set; }

    /// <summary>
    /// 岗位标识
    /// </summary>
    public string PostId { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 上班时间
    /// </summary>
    public string? Times { get; set; }

    /// <summary>
    /// 班次标识
    /// </summary>
    public string TypeId { get; set; } = string.Empty;

    /// <summary>
    /// 班次名称
    /// </summary>
    public string TypeName { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }
}