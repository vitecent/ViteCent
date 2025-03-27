#region

using MediatR;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseResource;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseResourceEntityArgs : SearchArgs, IRequest<List<BaseResourceEntity>>
{
}