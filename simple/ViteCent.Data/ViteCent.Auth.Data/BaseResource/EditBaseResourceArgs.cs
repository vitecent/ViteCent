namespace ViteCent.Auth.Data.BaseResource;

/// <summary>
/// 编辑资源信息参数
/// </summary>
[Serializable]
public class EditBaseResourceArgs : AddBaseResourceArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}