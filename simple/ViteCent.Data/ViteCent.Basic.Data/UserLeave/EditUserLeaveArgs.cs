namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// 编辑请假申请参数
/// </summary>
[Serializable]
public class EditUserLeaveArgs : AddUserLeaveArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}