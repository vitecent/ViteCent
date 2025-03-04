using ViteCent.Core.Enums;

namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class NextIdentifyArg : BaseArgs
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Count { get; set; } = 1;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Prefix { get; set; } = "";

    /// <summary>
    /// </summary>
    public string Suffix { get; set; } = "";

    /// <summary>
    /// </summary>
    public IdentifyEnum Type { get; set; } = IdentifyEnum.Day;
}