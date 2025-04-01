namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// 编辑职位信息参数
/// </summary>
[Serializable]
public class EditBasePositionArgs : AddBasePositionArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}