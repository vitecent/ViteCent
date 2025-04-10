/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Api;

/// <summary>
/// </summary>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// </summary>
    private void OverrideMap()
    {
        CreateMap<ListScheduleArgs, SearchScheduleEntityArgs>();
        CreateMap<ScheduleEntity, UserScheduleResult>();

        CreateMap<FirstScheduleArgs, GetScheduleEntityArgs>();
        CreateMap<LastScheduleArgs, GetScheduleEntityArgs>();
        CreateMap<ShiftScheduleTopicArgs, GetScheduleEntityArgs>();
        CreateMap<AddShiftScheduleArgs, GetScheduleEntityArgs>();
        CreateMap<EditShiftScheduleArgs, GetScheduleEntityArgs>();
    }
}