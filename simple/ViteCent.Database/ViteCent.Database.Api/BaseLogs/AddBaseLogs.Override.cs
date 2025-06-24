/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入日志信息信息相关的数据参数 引入核心

// 引入核心枚举类型

// 引入核心数据类型
using ViteCent.Core.Data;
using ViteCent.Database.Data.BaseLogs;

#endregion 引入命名空间

namespace ViteCent.Database.Api.BaseLogs;

/// <summary>
/// 新增日志信息接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理新增日志信息信息时的自定义逻辑</remarks>
public partial class AddBaseLogs
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <returns>处理结果</returns>
    internal static void OverrideInvoke(AddBaseLogsArgs args, BaseUserInfo user)
    {
        // 设置公司标识
        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        // 设置部门标识
        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty;
    }
}