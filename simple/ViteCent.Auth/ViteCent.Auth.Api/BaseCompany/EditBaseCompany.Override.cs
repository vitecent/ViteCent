/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

// 引入公司信息相关的数据传输对象
using ViteCent.Auth.Data.BaseCompany;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Api.BaseCompany;

/// <summary>
/// 编辑公司信息接口拓展
/// </summary>
/// 该部分类主要负责处理编辑公司信息时的自定义逻辑
/// </remarks>
public partial class EditBaseCompany
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(EditBaseCompanyArgs args, BaseUserInfo user)
    {
    }
}