/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * 此文件用于扩展公司信息分页查询的自定义处理逻辑
 */

#region 引入命名空间

// 引入公司实体相关命名空间
using ViteCent.Auth.Entity.BaseCompany;
// 引入核心数据层命名空间，提供基础数据结构
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// 公司信息分页应用拓展类
/// 用于处理公司信息的分页查询，支持自定义查询条件和权限过滤
/// 通过partial类的方式实现，便于代码分离和维护
/// </summary>
public partial class PageBaseCompany
{
    /// <summary>
    /// 重写处理方法，用于扩展公司信息查询的自定义逻辑
    /// </summary>
    /// <param name="args">公司信息查询参数，包含分页、排序和筛选条件</param>
    /// <param name="user">当前操作用户信息，用于权限验证和数据过滤</param>
    /// <remarks>
    /// 此方法可以实现以下功能：
    /// 1. 添加自定义的查询条件
    /// 2. 根据用户权限过滤数据
    /// 3. 处理特殊的业务规则
    /// </remarks>
    private void OverrideHandle(SearchBaseCompanyEntityArgs args, BaseUserInfo user)
    {
        
    }
}