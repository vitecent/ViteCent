/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// 获取用户信息模型参数
/// </summary>
[Serializable]
public class GetBaseUserEntityArgs : IRequest<BaseUserEntity>
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
    /// 职位标识
    /// </summary>
    public string PositionId { get; set; } = string.Empty;
}