namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class BaseResourceInfo
{
    /// <summary>
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public List<BaseOperationInfo> Operations { get; set; } = [];

    /// <summary>
    /// </summary>
    public int Sequence { get; set; }
}