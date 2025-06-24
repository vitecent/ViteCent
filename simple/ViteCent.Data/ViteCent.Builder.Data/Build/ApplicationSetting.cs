namespace ViteCent.Builder.Data.Build;

/// <summary>
/// </summary>
public class ApplicationSetting
{
    /// <summary>
    /// </summary>
    public string Guid { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = "Application";

    /// <summary>
    /// </summary>
    public bool Invoke { get; set; }

    /// <summary>
    /// </summary>
    public string InvokeProjrect { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string InvokeService { get; set; } = string.Empty;
}