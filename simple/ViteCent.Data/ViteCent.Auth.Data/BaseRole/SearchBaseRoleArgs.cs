#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// 搜索角色信息参数
/// </summary>
[Serializable]
public class SearchBaseRoleArgs : SearchArgs, IRequest<PageResult<BaseRoleResult>>
{
}