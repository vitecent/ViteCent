#region

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public class PreSearchScheduleArgs : BaseArgs
{
    /// <summary>
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// </summary>
    public DateTime StartTime { get; set; }
}