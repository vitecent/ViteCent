namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class BaseSystem
{
    /// <summary>
    /// </summary>
    /// <value>The code.</value>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The identifier.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The resources.</value>
    public List<BaseResource> Resources { get; set; } = [];

    /// <summary>
    /// </summary>
    /// <value>The sequence.</value>
    public int Sequence { get; set; }
}