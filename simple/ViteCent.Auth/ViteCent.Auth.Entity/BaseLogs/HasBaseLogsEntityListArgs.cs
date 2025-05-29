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

namespace ViteCent.Auth.Entity.BaseLogs;

/// <summary>
/// 批量职位信息判重模型参数
/// </summary>
[Serializable]
public class HasBaseLogsEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public List<string> CompanyIds { get; set; } = [];

    /// <summary>
    /// 部门标识
    /// </summary>
    public List<string> DepartmentIds { get; set; } = [];

    /// <summary>
    /// 操作标识
    /// </summary>
    public List<string> OperationIds { get; set; } = [];

    /// <summary>
    /// 资源标识
    /// </summary>
    public List<string> ResourceIds { get; set; } = [];

    /// <summary>
    /// 系统标识
    /// </summary>
    public List<string> SystemIds { get; set; } = [];
}