#region

using MediatR;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseUserEntityArgs : SearchArgs, IRequest<List<BaseUserEntity>>
{
}