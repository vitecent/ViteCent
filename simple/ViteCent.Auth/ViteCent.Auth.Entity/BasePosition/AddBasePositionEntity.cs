#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// 新增职位信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_position")]
public class AddBasePositionEntity : BasePositionEntity
{
}