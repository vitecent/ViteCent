/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入用户角色信息相关的数据参数
using ViteCent.Auth.Data.BaseUserRole;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Api.BaseUserRole;

/// <summary>
/// 分页查询用户角色接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理分页查询用户角色信息时的自定义逻辑</remarks>
public partial class PageBaseUserRole
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <returns>处理结果</returns>
    private static void OverrideInvoke(SearchBaseUserRoleArgs args, BaseUserInfo user)
    {
        // 添加公司标识查询条件，用于数据权限控制
        args.AddCompanyId(user);
    }
}