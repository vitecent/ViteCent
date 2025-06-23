#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
[Serializable]
public class SignScheduleArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public int Model { get; set; } = 1;

    /// <summary>
    /// 
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string SignTimes { get; set; } = string.Empty;
}