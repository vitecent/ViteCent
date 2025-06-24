/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 FluentValidation 核心
using FluentValidation;

#endregion 引入命名空间

namespace ViteCent.Database.Data.BaseDatabase;

/// <summary>
/// 验证数据库信息拓展
/// </summary>
public partial class BaseDatabaseValidator : AbstractValidator<AddBaseDatabaseArgs>
{
    /// <summary>
    /// 验证数据库信息
    /// </summary>
    /// <param name="validate">是否验证</param>
    private void OverrideValidator(bool validate = false)
    {
    }
}