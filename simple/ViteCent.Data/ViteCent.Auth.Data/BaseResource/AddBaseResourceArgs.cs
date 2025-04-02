/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseResource;

/// <summary>
/// 新增资源信息参数
/// </summary>
[Serializable]
public class AddBaseResourceArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    public string Abbreviation { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 颜色
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 简介
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 级别
    /// </summary>
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 系统标识
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}