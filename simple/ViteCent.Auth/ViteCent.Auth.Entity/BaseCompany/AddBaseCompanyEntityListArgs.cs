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
/// 批量新增公司信息参数
/// </summary>
[Serializable]
public class AddBaseCompanyEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public List<AddBaseCompanyEntity> Items = [];
}