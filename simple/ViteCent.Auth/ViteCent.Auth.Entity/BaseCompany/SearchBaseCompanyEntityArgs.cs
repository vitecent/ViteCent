#region

using MediatR;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseCompanyEntityArgs : SearchArgs, IRequest<List<BaseCompanyEntity>>
{
}