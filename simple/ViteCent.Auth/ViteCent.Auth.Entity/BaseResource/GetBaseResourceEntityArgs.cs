#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseResource;

/// <summary>
/// 获取资源信息数据参数
/// </summary>
[Serializable]
public class GetBaseResourceEntityArgs : IRequest<BaseResourceEntity>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 系统标识
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}