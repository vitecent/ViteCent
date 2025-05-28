/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

// 引入公司信息相关的数据传输对象
using ViteCent.Basic.Data.UserLeave;

// 引入核心
using ViteCent.Core;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Api.UserLeave;

/// <summary>
/// 编辑请假申请接口拓展
/// </summary>
/// <remarks>
/// 该部分类主要负责处理编辑公司信息时的自定义逻辑
/// </remarks>
public partial class EditUserLeave
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(EditUserLeaveArgs args, BaseUserInfo user)
    {
        // 如果用户不是超级管理员，则设置公司标识
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = user.Company.Id;

        // 如果用户不是超级管理员，则设置部门标识
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = user.Department.Id;

    }
}