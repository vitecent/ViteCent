/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入日志信息相关的数据参数
using ViteCent.Basic.Data.BaseLogs;

// 引入职位信息相关的数据参数
using ViteCent.Basic.Data.BasePost;

// 引入补卡申请相关的数据参数
using ViteCent.Basic.Data.RepairSchedule;

// 引入排班信息相关的数据参数
using ViteCent.Basic.Data.Schedule;

// 引入基础排班相关的数据参数
using ViteCent.Basic.Data.ScheduleType;

// 引入换班申请相关的数据参数
using ViteCent.Basic.Data.ShiftSchedule;

// 引入请假申请相关的数据参数
using ViteCent.Basic.Data.UserLeave;

// 引入调休申请相关的数据参数
using ViteCent.Basic.Data.UserRest;

// 引入日志信息相关的数据模型对象
using ViteCent.Basic.Entity.BaseLogs;

// 引入职位信息相关的数据模型对象
using ViteCent.Basic.Entity.BasePost;

// 引入补卡申请相关的数据模型对象
using ViteCent.Basic.Entity.RepairSchedule;

// 引入排班信息相关的数据模型对象
using ViteCent.Basic.Entity.Schedule;

// 引入基础排班相关的数据模型对象
using ViteCent.Basic.Entity.ScheduleType;

// 引入换班申请相关的数据模型对象
using ViteCent.Basic.Entity.ShiftSchedule;

// 引入请假申请相关的数据模型对象
using ViteCent.Basic.Entity.UserLeave;

// 引入调休申请相关的数据模型对象
using ViteCent.Basic.Entity.UserRest;

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Basic.Api;

/// <summary>
/// AutoMapper对象映射配置类
/// </summary>
/// <remarks>
/// 该类负责配置ViteCent.Basic模块中所有需要的对象映射关系，主要功能包括：
/// 1. ViteCent.Basic请求参数与模型参数之间的映射
/// 2. 继承自BaseMapperConfig基类，实现自动化的对象映射配置
/// </remarks>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 配置对象映射关系
    /// </summary>
    /// <remarks>在此方法中配置所有需要的对象映射规则</remarks>
    public override void Map()
    {
        #region 日志信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseLogsArgs, AddBaseLogsEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseLogsArgs, SearchBaseLogsEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseLogsEntity, BaseLogsResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseLogsArgs, GetBaseLogsEntityArgs>();
        CreateMap<BaseLogsEntity, DeleteBaseLogsEntity>();

        #endregion 日志信息对象映射配置

        #region 职位信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBasePostArgs, AddBasePostEntity>();

        // 编辑对象映射配置
        CreateMap<EditBasePostArgs, GetBasePostEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBasePostArgs, GetBasePostEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBasePostArgs, SearchBasePostEntityArgs>();

        // 获取对象映射配置
        CreateMap<BasePostEntity, BasePostResult>();

        // 删除对象映射配置
        CreateMap<DeleteBasePostArgs, GetBasePostEntityArgs>();
        CreateMap<BasePostEntity, DeleteBasePostEntity>();

        #endregion 职位信息对象映射配置

        #region 补卡申请对象映射配置

        // 新增对象映射配置
        CreateMap<AddRepairScheduleArgs, AddRepairScheduleEntity>();

        // 编辑对象映射配置
        CreateMap<EditRepairScheduleArgs, GetRepairScheduleEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetRepairScheduleArgs, GetRepairScheduleEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchRepairScheduleArgs, SearchRepairScheduleEntityArgs>();

        // 获取对象映射配置
        CreateMap<RepairScheduleEntity, RepairScheduleResult>();

        // 删除对象映射配置
        CreateMap<DeleteRepairScheduleArgs, GetRepairScheduleEntityArgs>();
        CreateMap<RepairScheduleEntity, DeleteRepairScheduleEntity>();

        #endregion 补卡申请对象映射配置

        #region 排班信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddScheduleArgs, AddScheduleEntity>();

        // 编辑对象映射配置
        CreateMap<EditScheduleArgs, GetScheduleEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetScheduleArgs, GetScheduleEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchScheduleArgs, SearchScheduleEntityArgs>();

        // 获取对象映射配置
        CreateMap<ScheduleEntity, ScheduleResult>();

        // 删除对象映射配置
        CreateMap<DeleteScheduleArgs, GetScheduleEntityArgs>();
        CreateMap<ScheduleEntity, DeleteScheduleEntity>();

        #endregion 排班信息对象映射配置

        #region 基础排班对象映射配置

        // 新增对象映射配置
        CreateMap<AddScheduleTypeArgs, AddScheduleTypeEntity>();

        // 编辑对象映射配置
        CreateMap<EditScheduleTypeArgs, GetScheduleTypeEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetScheduleTypeArgs, GetScheduleTypeEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchScheduleTypeArgs, SearchScheduleTypeEntityArgs>();

        // 获取对象映射配置
        CreateMap<ScheduleTypeEntity, ScheduleTypeResult>();

        // 删除对象映射配置
        CreateMap<DeleteScheduleTypeArgs, GetScheduleTypeEntityArgs>();
        CreateMap<ScheduleTypeEntity, DeleteScheduleTypeEntity>();

        #endregion 基础排班对象映射配置

        #region 换班申请对象映射配置

        // 新增对象映射配置
        CreateMap<AddShiftScheduleArgs, AddShiftScheduleEntity>();

        // 编辑对象映射配置
        CreateMap<EditShiftScheduleArgs, GetShiftScheduleEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetShiftScheduleArgs, GetShiftScheduleEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchShiftScheduleArgs, SearchShiftScheduleEntityArgs>();

        // 获取对象映射配置
        CreateMap<ShiftScheduleEntity, ShiftScheduleResult>();

        // 删除对象映射配置
        CreateMap<DeleteShiftScheduleArgs, GetShiftScheduleEntityArgs>();
        CreateMap<ShiftScheduleEntity, DeleteShiftScheduleEntity>();

        #endregion 换班申请对象映射配置

        #region 请假申请对象映射配置

        // 新增对象映射配置
        CreateMap<AddUserLeaveArgs, AddUserLeaveEntity>();

        // 编辑对象映射配置
        CreateMap<EditUserLeaveArgs, GetUserLeaveEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetUserLeaveArgs, GetUserLeaveEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchUserLeaveArgs, SearchUserLeaveEntityArgs>();

        // 获取对象映射配置
        CreateMap<UserLeaveEntity, UserLeaveResult>();

        // 删除对象映射配置
        CreateMap<DeleteUserLeaveArgs, GetUserLeaveEntityArgs>();
        CreateMap<UserLeaveEntity, DeleteUserLeaveEntity>();

        #endregion 请假申请对象映射配置

        #region 调休申请对象映射配置

        // 新增对象映射配置
        CreateMap<AddUserRestArgs, AddUserRestEntity>();

        // 编辑对象映射配置
        CreateMap<EditUserRestArgs, GetUserRestEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetUserRestArgs, GetUserRestEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchUserRestArgs, SearchUserRestEntityArgs>();

        // 获取对象映射配置
        CreateMap<UserRestEntity, UserRestResult>();

        // 删除对象映射配置
        CreateMap<DeleteUserRestArgs, GetUserRestEntityArgs>();
        CreateMap<UserRestEntity, DeleteUserRestEntity>();

        #endregion 调休申请对象映射配置

        // 其他对象映射配置
        OverrideMap();
    }
}