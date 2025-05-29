/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

// 引入公司信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心

// 引入核心枚举类型
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 编辑排班信息接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理编辑公司信息时的自定义逻辑</remarks>
public partial class EditSchedule
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(EditScheduleArgs args, BaseUserInfo user)
    {
        // 如果用户不是超级管理员，则设置公司标识
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = user?.Company?.Id ?? string.Empty;

        // 如果用户不是超级管理员，则设置部门标识
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = user?.Department?.Id ?? string.Empty; ;
    }
}