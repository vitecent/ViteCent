/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入用户信息信息相关的数据参数
using ViteCent.Auth.Data.BaseUser;

// 引入核心数据类型
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion 引入命名空间

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 分页查询用户信息接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理分页查询用户信息信息时的自定义逻辑</remarks>
public partial class PageBaseUser
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <returns>处理结果</returns>
    private static void OverrideInvoke(SearchBaseUserArgs args, BaseUserInfo user)
    {
        // 添加公司标识查询条件，用于数据权限控制
        args.AddCompanyId(user);

        //添加 Sort 倒序排序
        args.AddOrder("Sort");

        // 过滤超级管理员
        args.AddArgs("IsSuper", ((int)YesNoEnum.Yes).ToString(), SearchEnum.NoEqual);
    }
}