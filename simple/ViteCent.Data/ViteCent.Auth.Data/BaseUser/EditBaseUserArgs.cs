namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// 编辑用户信息参数
/// </summary>
[Serializable]
public class EditBaseUserArgs : AddBaseUserArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}