/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

// 引入公司信息相关的数据传输对象
using ViteCent.Auth.Data.BaseUser;

// 引入核心
using ViteCent.Core;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 编辑用户信息接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理编辑公司信息时的自定义逻辑</remarks>
public partial class EditBaseUser
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(EditBaseUserArgs args, BaseUserInfo user)
    {
        args.IsSuper = (int)YesNoEnum.No;

        // 设置公司标识
        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        // 设置部门标识
        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty; ;

        // 设置职位标识
        if (string.IsNullOrEmpty(args.PositionId))
            args.PositionId = user?.Position?.Id ?? string.Empty;

        if (string.IsNullOrEmpty(args.Username) && !string.IsNullOrWhiteSpace(args.RealName))
            args.Username = args.RealName.GetPinYin().ToCamelCase();
    }
}