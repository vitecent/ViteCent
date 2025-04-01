namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// 编辑系统信息参数
/// </summary>
[Serializable]
public class EditBaseSystemArgs : AddBaseSystemArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}