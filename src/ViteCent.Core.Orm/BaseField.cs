namespace ViteCent.Core.Orm;

/// <summary>
///     Class BaseField.
/// </summary>
public class BaseField
{
    /// <summary>
    ///     Default
    /// </summary>
    public string Default { get; set; } = string.Empty;

    /// <summary>
    ///     Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Identity
    /// </summary>
    public bool Identity { get; set; }

    /// <summary>
    ///     Length
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Nullable
    /// </summary>
    public bool Nullable { get; set; }

    /// <summary>
    ///     PrimaryKey
    /// </summary>
    public bool PrimaryKey { get; set; }

    /// <summary>
    ///     Type
    /// </summary>
    public string Type { get; set; } = string.Empty;
}