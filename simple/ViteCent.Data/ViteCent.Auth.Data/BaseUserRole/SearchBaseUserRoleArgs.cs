#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// 搜索用户角色参数
/// </summary>
[Serializable]
public class SearchBaseUserRoleArgs : SearchArgs, IRequest<PageResult<BaseUserRoleResult>>
{
}