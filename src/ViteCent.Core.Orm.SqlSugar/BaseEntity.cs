using SqlSugar;

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
///     Class BaseEntity. Implements the <see cref="ViteCent.Core.Orm.IBaseEntity" />
/// </summary>
/// <seealso cref="ViteCent.Core.Orm.IBaseEntity" />
[Serializable]
public class BaseEntity : IBaseEntity
{
    /// <summary>
    ///     创建时间
    /// </summary>
    [SugarColumn(ColumnName = "createTime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///     创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator")]
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    ///     版本号
    /// </summary>
    [SugarColumn(ColumnName = "dataVersion", IsEnableUpdateVersionValidation = true, IsOnlyIgnoreInsert = true,
        IsOnlyIgnoreUpdate = true)]
    public DateTime DataVersion { get; set; }

    /// <summary>
    ///     标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     状态
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int Status { get; set; }

    /// <summary>
    ///     修改人
    /// </summary>
    [SugarColumn(ColumnName = "updater")]
    public string Updater { get; set; }

    /// <summary>
    ///     修改时间
    /// </summary>
    [SugarColumn(ColumnName = "updateTime")]
    public DateTime UpdateTime { get; set; }
}