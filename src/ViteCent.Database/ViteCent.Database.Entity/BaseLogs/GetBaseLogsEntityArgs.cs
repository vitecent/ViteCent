/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

#endregion 引入命名空间

namespace ViteCent.Database.Entity.BaseLogs;

/// <summary>
/// 获取日志信息模型参数
/// </summary>
[Serializable]
public class GetBaseLogsEntityArgs : IRequest<BaseLogsEntity>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 操作标识
    /// </summary>
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 核心标识
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}