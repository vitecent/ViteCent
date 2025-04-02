/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// 删除操作信息参数
/// </summary>
[Serializable]
public class DeleteBaseOperationArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 系统标识
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}