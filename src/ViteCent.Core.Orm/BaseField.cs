namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class BaseField
{
    /// <summary>
    /// </summary>
    public string Default { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public bool Identity { get; set; }

    /// <summary>
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public bool Nullable { get; set; }

    /// <summary>
    /// </summary>
    public bool PrimaryKey { get; set; }

    /// <summary>
    /// </summary>
    public string Type { get; set; } = string.Empty;
}