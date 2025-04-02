/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 删除公司信息数据参数
/// </summary>
[Serializable]
public class DeleteBaseCompanyEntityArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}