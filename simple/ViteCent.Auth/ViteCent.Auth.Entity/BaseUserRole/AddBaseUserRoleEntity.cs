#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseUserRole;

/// <summary>
/// 新增用户角色数据参数
/// </summary>
[Serializable]
[SugarTable("base_user_role")]
public class AddBaseUserRoleEntity : BaseUserRoleEntity
{
}