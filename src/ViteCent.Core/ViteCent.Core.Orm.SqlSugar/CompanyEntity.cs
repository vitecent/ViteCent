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
    ///     companyId
    /// </summary>
    [SugarColumn(ColumnName = "companyId")]
    public string CompanyId { get; set; } = string.Empty;
}