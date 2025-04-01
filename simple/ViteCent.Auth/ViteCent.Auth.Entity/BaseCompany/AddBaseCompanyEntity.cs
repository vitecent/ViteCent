#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 新增公司信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_company")]
public class AddBaseCompanyEntity : BaseCompanyEntity
{
}