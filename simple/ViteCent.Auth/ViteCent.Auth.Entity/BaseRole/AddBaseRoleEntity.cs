#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseRole;

/// <summary>
/// 新增角色信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_role")]
public class AddBaseRoleEntity : BaseRoleEntity
{
}