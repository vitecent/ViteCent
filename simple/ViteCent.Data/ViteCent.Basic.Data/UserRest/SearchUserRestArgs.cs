#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// </summary>
[Serializable]
public class SearchUserRestArgs : SearchArgs, IRequest<PageResult<UserRestResult>>
{
}