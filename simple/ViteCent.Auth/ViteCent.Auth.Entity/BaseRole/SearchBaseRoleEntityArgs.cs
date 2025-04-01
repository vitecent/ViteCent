#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseRole;

/// <summary>
/// 搜索角色信息数据参数
/// </summary>
[Serializable]
public class SearchBaseRoleEntityArgs : SearchArgs, IRequest<List<BaseRoleEntity>>
{
}