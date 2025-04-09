/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// 获取职位信息参数
/// </summary>
[Serializable]
public class GetBasePositionEntityArgs : IRequest<BasePositionEntity>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}