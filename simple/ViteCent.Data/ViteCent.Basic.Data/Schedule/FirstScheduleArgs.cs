#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// 上班参数
/// </summary>
[Serializable]
public class FirstScheduleArgs : BaseArgs, IRequest<BaseResult>
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
    /// 打卡类型
    /// </summary>
    public int Model { get; set; } = 1;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}