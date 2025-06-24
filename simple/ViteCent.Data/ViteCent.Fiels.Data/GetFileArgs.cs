#region 引入命名空间

using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Files.Data;

/// <summary>
/// 获取文件参数类，用于文件相关操作的参数传递
/// </summary>
public class GetFileArgs : BaseArgs
{
    /// <summary>
    /// 文件路径，标识要获取的文件在系统中的相对路径
    /// </summary>
    public string Path { get; set; } = string.Empty;
}