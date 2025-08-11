/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

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
    /// 岗位标识
    /// </summary>
    public string PostId { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 排班时间
    /// </summary>
    public DateTime SceduleTimes { get; set; }

    /// <summary>
    /// 打卡时间
    /// </summary>
    public string? SignTimes { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 上班时间
    /// </summary>
    public string Times { get; set; } = string.Empty;

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