#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// 新增用户信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_user")]
public class AddBaseUserEntity : BaseUserEntity
{
}