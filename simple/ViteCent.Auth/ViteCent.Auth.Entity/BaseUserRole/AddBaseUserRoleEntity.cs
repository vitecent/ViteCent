#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseUserRole;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_user_role")]
public class AddBaseUserRoleEntity : BaseUserRoleEntity
{

}