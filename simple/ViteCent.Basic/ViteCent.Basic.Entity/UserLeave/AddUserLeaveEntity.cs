#region

using SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// 新增请假申请数据参数
/// </summary>
[Serializable]
[SugarTable("user_leave")]
public class AddUserLeaveEntity : UserLeaveEntity
{
}