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
    /// <value>The type of the command.</value>
    public CommandEnum CommandType { get; set; }

    /// <summary>
    /// </summary>
    /// <value>The type of the data.</value>
    public DataEnum DataType { get; set; }

    /// <summary>
    /// </summary>
    /// <value>The Entity.</value>
    public dynamic Entity { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The parameters.</value>
    public object Parameters { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The SQL.</value>
    public string SQL { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The where.</value>
    public dynamic Where { get; set; } = string.Empty;
}