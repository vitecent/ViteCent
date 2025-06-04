/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入职位信息信息相关的数据参数
using ViteCent.Auth.Data.BasePosition;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Api.BasePosition;

/// <summary>
/// 分页查询职位信息接口拓展
/// </summary>
/// <remarks>该部分类主要负责处理分页查询职位信息信息时的自定义逻辑</remarks>
public partial class PageBasePosition
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    private static void OverrideInvoke(SearchBasePositionArgs args, BaseUserInfo user)
    {
        // 添加公司标识查询条件，用于数据权限控制
        args.AddCompanyId(user);
    }
}