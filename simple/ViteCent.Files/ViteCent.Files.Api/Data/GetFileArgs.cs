#region

using ViteCent.Core.Data;

#endregion

namespace ViteCent.Files.Api.Data;

/// <summary>
/// </summary>
public class GetFileArgs : BaseArgs
{
    /// <summary>
    /// </summary>
    public string Path { get; set; } = string.Empty;
}