#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// </summary>
[Serializable]
public class SearchUserRestEntityArgs : SearchArgs, IRequest<List<UserRestEntity>>
{
}