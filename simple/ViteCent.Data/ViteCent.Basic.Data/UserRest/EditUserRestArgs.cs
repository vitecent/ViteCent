namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// 编辑调休申请参数
/// </summary>
[Serializable]
public class EditUserRestArgs : AddUserRestArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}