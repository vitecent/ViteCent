/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// 批量职位信息判重参数
/// </summary>
[Serializable]
public class HasBasePositionEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public List<string> Codes { get; set; } = [];

    /// <summary>
    /// 公司标识
    /// </summary>
    public List<string> CompanyIds { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> Names { get; set; } = [];
}