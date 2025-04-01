namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// 编辑公司信息参数
/// </summary>
[Serializable]
public class EditBaseCompanyArgs : AddBaseCompanyArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}