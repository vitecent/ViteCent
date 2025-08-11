/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Data.BaseLogs;

/// <summary>
/// 新增日志信息参数
/// </summary>
[Serializable]
public class AddBaseLogsArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 数据
    /// </summary>
    public string? Args { get; set; }

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 操作标识
    /// </summary>
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// 操作名称
    /// </summary>
    public string OperationName { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 资源名称
    /// </summary>
    public string? ResourceName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 系统标识
    /// </summary>
    public string SystemId { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称
    /// </summary>
    public string? SystemName { get; set; }
}