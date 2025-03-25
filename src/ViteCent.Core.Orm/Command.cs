#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class Command
{
    /// <summary>
    /// </summary>
    public CommandEnum CommandType { get; set; }

    /// <summary>
    /// </summary>
    public DataEnum DataType { get; set; }

    /// <summary>
    /// </summary>
    public dynamic Entity { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public object Parameters { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string SQL { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public dynamic Where { get; set; } = string.Empty;
}