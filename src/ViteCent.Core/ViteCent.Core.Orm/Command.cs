#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
///     Class Command.
/// </summary>
public class Command
{
    /// <summary>
    ///     Gets or sets the type of the command.
    /// </summary>
    /// <value>The type of the command.</value>
    public CommandEnum CommandType { get; set; }

    /// <summary>
    ///     Gets or sets the type of the data.
    /// </summary>
    /// <value>The type of the data.</value>
    public DataEnum DataType { get; set; }

    /// <summary>
    ///     Gets or sets the Entity.
    /// </summary>
    /// <value>The Entity.</value>
    public dynamic Entity { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the parameters.
    /// </summary>
    /// <value>The parameters.</value>
    public object Parameters { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the SQL.
    /// </summary>
    /// <value>The SQL.</value>
    public string SQL { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the where.
    /// </summary>
    /// <value>The where.</value>
    public dynamic Where { get; set; } = string.Empty;
}