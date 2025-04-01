#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// 获取职位信息参数
/// </summary>
[Serializable]
public class GetBasePositionArgs : BaseArgs, IRequest<DataResult<BasePositionResult>>
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