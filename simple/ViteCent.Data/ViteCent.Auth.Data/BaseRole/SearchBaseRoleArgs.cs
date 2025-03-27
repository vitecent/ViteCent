#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseRoleArgs : SearchArgs, IRequest<PageResult<BaseRoleResult>>
{
}