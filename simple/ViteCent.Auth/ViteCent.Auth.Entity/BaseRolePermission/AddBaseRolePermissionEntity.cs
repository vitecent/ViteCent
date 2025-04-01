#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// 新增角色权限数据参数
/// </summary>
[Serializable]
[SugarTable("base_role_permission")]
public class AddBaseRolePermissionEntity : BaseRolePermissionEntity
{
}