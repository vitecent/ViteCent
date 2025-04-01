namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// 编辑操作信息参数
/// </summary>
[Serializable]
public class EditBaseOperationArgs : AddBaseOperationArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}