#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseUserRole;

/// <summary>
/// 搜索用户角色数据参数
/// </summary>
[Serializable]
public class SearchBaseUserRoleEntityArgs : SearchArgs, IRequest<List<BaseUserRoleEntity>>
{
}