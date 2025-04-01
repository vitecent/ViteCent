#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 获取公司信息数据参数
/// </summary>
[Serializable]
public class GetBaseCompanyEntityArgs : IRequest<BaseCompanyEntity>
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}