using ViteCent.Core.Data;

namespace ViteCent.Builder.Data.Build;

/// <summary>
/// </summary>
public class Setting : BaseArgs
{
    /// <summary>
    /// </summary>
    public string AddName { get; set; } = "Add";

    /// <summary>
    /// </summary>
    public ApiSetting Api { get; set; } = new();

    /// <summary>
    /// </summary>
    public ApplicationSetting Application { get; set; } = new();

    /// <summary>
    /// </summary>
    public DataSetting Data { get; set; } = new();

    /// <summary>
    /// </summary>
    public string DeleteName { get; set; } = "Delete";

    /// <summary>
    /// </summary>
    public string DisableName { get; set; } = "Disable";

    /// <summary>
    /// </summary>
    public DomainSetting Domain { get; set; } = new();

    /// <summary>
    /// </summary>
    public string EditName { get; set; } = "Edit";

    /// <summary>
    /// </summary>
    public string EnableName { get; set; } = "Enable";

    /// <summary>
    /// </summary>
    public EntitySetting Entity { get; set; } = new();

    /// <summary>
    /// </summary>
    public DatabaseSetting Database { get; set; } = new();

    /// <summary>
    /// </summary>
    public string GetName { get; set; } = "Get";

    /// <summary>
    /// </summary>
    public string Guid { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string HasName { get; set; } = "Has";

    /// <summary>
    /// </summary>
    public string PageName { get; set; } = "Page";

    /// <summary>
    /// </summary>
    public string ProjrectName { get; set; } = "ViteCent";

    /// <summary>
    /// </summary>
    public string Root { get; set; } = @"E:\Server\ViteCent\ViteCent.Core\simple";

    /// <summary>
    /// </summary>
    public string SolutionName { get; set; } = "";

    /// <summary>
    /// </summary>
    public string SrcName { get; set; } = "";
}