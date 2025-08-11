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

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// 批量用户信息判重参数
/// </summary>
[Serializable]
public class HasBaseUserEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public List<string> Codes { get; set; } = [];

    /// <summary>
    /// 公司标识
    /// </summary>
    public List<string> CompanyIds { get; set; } = [];

    /// <summary>
    /// 部门标识
    /// </summary>
    public List<string> DepartmentIds { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> Emails { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> IdCards { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> Names { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> Phones { get; set; } = [];

    /// <summary>
    /// 职位标识
    /// </summary>
    public List<string> PositionIds { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> RealNames { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> Usernames { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> UserNos { get; set; } = [];
}