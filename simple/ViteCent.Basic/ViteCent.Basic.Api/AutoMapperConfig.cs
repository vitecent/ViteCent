#region

using ViteCent.Basic.Data.Attendance;
using ViteCent.Basic.Data.RepairAttendance;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Entity.Attendance;
using ViteCent.Basic.Entity.RepairAttendance;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Api;

/// <summary>
/// </summary>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// </summary>
    public override void Map()
    {
        #region Attendance

        CreateMap<AddAttendanceArgs, AddAttendanceEntity>();
        CreateMap<EditAttendanceArgs, GetAttendanceEntityArgs>();
        CreateMap<GetAttendanceArgs, GetAttendanceEntityArgs>();
        CreateMap<SearchAttendanceArgs, SearchAttendanceEntityArgs>();
        CreateMap<AttendanceEntity, AttendanceResult>();
        CreateMap<DeleteAttendanceArgs, DeleteAttendanceEntityArgs>();

        #endregion

        #region RepairAttendance

        CreateMap<AddRepairAttendanceArgs, AddRepairAttendanceEntity>();
        CreateMap<EditRepairAttendanceArgs, GetRepairAttendanceEntityArgs>();
        CreateMap<GetRepairAttendanceArgs, GetRepairAttendanceEntityArgs>();
        CreateMap<SearchRepairAttendanceArgs, SearchRepairAttendanceEntityArgs>();
        CreateMap<RepairAttendanceEntity, RepairAttendanceResult>();
        CreateMap<DeleteRepairAttendanceArgs, DeleteRepairAttendanceEntityArgs>();

        #endregion

        #region Schedule

        CreateMap<AddScheduleArgs, AddScheduleEntity>();
        CreateMap<EditScheduleArgs, GetScheduleEntityArgs>();
        CreateMap<GetScheduleArgs, GetScheduleEntityArgs>();
        CreateMap<SearchScheduleArgs, SearchScheduleEntityArgs>();
        CreateMap<ScheduleEntity, ScheduleResult>();
        CreateMap<DeleteScheduleArgs, DeleteScheduleEntityArgs>();

        #endregion

        #region ScheduleType

        CreateMap<AddScheduleTypeArgs, AddScheduleTypeEntity>();
        CreateMap<EditScheduleTypeArgs, GetScheduleTypeEntityArgs>();
        CreateMap<GetScheduleTypeArgs, GetScheduleTypeEntityArgs>();
        CreateMap<SearchScheduleTypeArgs, SearchScheduleTypeEntityArgs>();
        CreateMap<ScheduleTypeEntity, ScheduleTypeResult>();
        CreateMap<DeleteScheduleTypeArgs, DeleteScheduleTypeEntityArgs>();

        #endregion

        #region ShiftSchedule

        CreateMap<AddShiftScheduleArgs, AddShiftScheduleEntity>();
        CreateMap<EditShiftScheduleArgs, GetShiftScheduleEntityArgs>();
        CreateMap<GetShiftScheduleArgs, GetShiftScheduleEntityArgs>();
        CreateMap<SearchShiftScheduleArgs, SearchShiftScheduleEntityArgs>();
        CreateMap<ShiftScheduleEntity, ShiftScheduleResult>();
        CreateMap<DeleteShiftScheduleArgs, DeleteShiftScheduleEntityArgs>();

        #endregion

        #region UserLeave

        CreateMap<AddUserLeaveArgs, AddUserLeaveEntity>();
        CreateMap<EditUserLeaveArgs, GetUserLeaveEntityArgs>();
        CreateMap<GetUserLeaveArgs, GetUserLeaveEntityArgs>();
        CreateMap<SearchUserLeaveArgs, SearchUserLeaveEntityArgs>();
        CreateMap<UserLeaveEntity, UserLeaveResult>();
        CreateMap<DeleteUserLeaveArgs, DeleteUserLeaveEntityArgs>();

        #endregion

        OverrideMap();
    }
}