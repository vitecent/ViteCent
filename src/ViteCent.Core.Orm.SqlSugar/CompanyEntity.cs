using SqlSugar;

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
///     Class BaseEntity. Implements the <see cref="ViteCent.Core.Orm.IBaseEntity" />
/// </summary>
/// <seealso cref="ViteCent.Core.Orm.IBaseEntity" />
[Serializable]
public class CompanyEntity : BaseEntity
{
    /// <summary>
    ///     公司标识
    /// </summary>
    [SugarColumn(ColumnName = "companyId")]
    public string CompanyId { get; set; } = string.Empty;
}