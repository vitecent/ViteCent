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
    ///     createTime
    /// </summary>
    [SugarColumn(ColumnName = "createTime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///     creator
    /// </summary>
    [SugarColumn(ColumnName = "creator")]
    public string Creator { get; set; }

    /// <summary>
    ///     on update CURRENT_TIMESTAMP
    /// </summary>
    [SugarColumn(ColumnName = "dataVersion", IsEnableUpdateVersionValidation = true, IsOnlyIgnoreInsert = true,
        IsOnlyIgnoreUpdate = true)]
    public DateTime DataVersion { get; set; }

    /// <summary>
    ///     id
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     status
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int Status { get; set; }

    /// <summary>
    ///     updater
    /// </summary>
    [SugarColumn(ColumnName = "updater")]
    public string Updater { get; set; }

    /// <summary>
    ///     on update CURRENT_TIMESTAMP
    /// </summary>
    [SugarColumn(ColumnName = "updateTime")]
    public DateTime UpdateTime { get; set; }
}