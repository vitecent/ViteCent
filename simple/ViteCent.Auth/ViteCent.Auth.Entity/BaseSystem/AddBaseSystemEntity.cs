#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseSystem;

/// <summary>
/// 新增系统信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_system")]
public class AddBaseSystemEntity : BaseSystemEntity
{
}