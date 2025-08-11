/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

// 引入公司信息相关的数据传输对象
using ViteCent.Basic.Data.RepairSchedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心

// 引入核心枚举类型

#endregion

namespace ViteCent.Basic.Api.RepairSchedule;

/// <summary>
/// 编辑补卡申请接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理编辑公司信息时的自定义逻辑</remarks>
public partial class EditRepairSchedule
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <returns>处理结果</returns>
    internal static void OverrideInvoke(EditRepairScheduleArgs args, BaseUserInfo user)
    {
        // 设置公司标识

        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        // 设置部门标识

        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty; ;
    }
}