#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// 删除公司信息参数
/// </summary>
[Serializable]
public class DeleteBaseCompanyArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}