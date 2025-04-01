namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// 编辑部门信息参数
/// </summary>
[Serializable]
public class EditBaseDepartmentArgs : AddBaseDepartmentArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}