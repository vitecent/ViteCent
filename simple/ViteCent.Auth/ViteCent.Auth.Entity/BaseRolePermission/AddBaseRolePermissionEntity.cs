#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_role_permission")]
public class AddBaseRolePermissionEntity : BaseRolePermissionEntity
{

}