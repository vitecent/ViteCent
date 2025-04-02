/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 */

#region

using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Api;

/// <summary>
/// 调休申请映射
/// </summary>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 映射
    /// </summary>
    public override void Map()
    {
        #region RepairSchedule

        CreateMap<AddRepairScheduleArgs, AddRepairScheduleEntity>();
        CreateMap<EditRepairScheduleArgs, GetRepairScheduleEntityArgs>();
        CreateMap<GetRepairScheduleArgs, GetRepairScheduleEntityArgs>();
        CreateMap<SearchRepairScheduleArgs, SearchRepairScheduleEntityArgs>();
        CreateMap<RepairScheduleEntity, RepairScheduleResult>();
        CreateMap<DeleteRepairScheduleArgs, DeleteRepairScheduleEntityArgs>();

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

        #region UserRest

        CreateMap<AddUserRestArgs, AddUserRestEntity>();
        CreateMap<EditUserRestArgs, GetUserRestEntityArgs>();
        CreateMap<GetUserRestArgs, GetUserRestEntityArgs>();
        CreateMap<SearchUserRestArgs, SearchUserRestEntityArgs>();
        CreateMap<UserRestEntity, UserRestResult>();
        CreateMap<DeleteUserRestArgs, DeleteUserRestEntityArgs>();

        #endregion

        OverrideMap();
    }
}