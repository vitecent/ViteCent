#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// 搜索公司信息参数
/// </summary>
[Serializable]
public class SearchBaseCompanyArgs : SearchArgs, IRequest<PageResult<BaseCompanyResult>>
{
}