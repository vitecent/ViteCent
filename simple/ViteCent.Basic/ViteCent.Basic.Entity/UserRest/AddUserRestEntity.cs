#region

using SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// 新增调休申请数据参数
/// </summary>
[Serializable]
[SugarTable("user_rest")]
public class AddUserRestEntity : UserRestEntity
{
}