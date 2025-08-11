/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Database.Entity.BaseLogs;

/// <summary>
/// 批量日志信息判重模型参数
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
    /// 核心标识
    /// </summary>
    public List<string> SystemIds { get; set; } = [];
}