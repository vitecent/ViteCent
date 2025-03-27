#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseCompanyArgs : SearchArgs, IRequest<PageResult<BaseCompanyResult>>
{
}