#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// 获取系统信息参数
/// </summary>
[Serializable]
public class GetBaseSystemArgs : BaseArgs, IRequest<DataResult<BaseSystemResult>>
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