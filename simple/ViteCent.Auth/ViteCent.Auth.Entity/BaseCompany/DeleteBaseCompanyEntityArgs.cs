#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 删除公司信息数据参数
/// </summary>
[Serializable]
public class DeleteBaseCompanyEntityArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}