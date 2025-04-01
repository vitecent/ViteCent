#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 搜索公司信息数据参数
/// </summary>
[Serializable]
public class SearchBaseCompanyEntityArgs : SearchArgs, IRequest<List<BaseCompanyEntity>>
{
}