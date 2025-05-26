/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// 获取请假申请模型参数
/// </summary>
[Serializable]
public class GetUserLeaveEntityArgs : IRequest<UserLeaveEntity>
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