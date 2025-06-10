#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public class GetScheduleTimeArgs : BaseArgs, IRequest<PageResult<ScheduleTimeResult>>
{
}