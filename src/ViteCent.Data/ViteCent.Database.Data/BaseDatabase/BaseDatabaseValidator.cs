/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 FluentValidation 核心
using FluentValidation;

#endregion 引入命名空间

namespace ViteCent.Database.Data.BaseDatabase;

/// <summary>
/// 数据库信息验证器
/// </summary>
[Serializable]
public partial class BaseDatabaseValidator : AbstractValidator<AddBaseDatabaseArgs>
{
    /// <summary>
    /// 验证数据库信息
    /// </summary>
    /// <param name="validate">是否验证</param>
    public BaseDatabaseValidator(bool validate = true)
    {
        // 验证参数不能为空
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        // 验证编码不能为空
        RuleFor(x => x.CharSet).NotNull().NotEmpty().WithMessage("编码不能为空");

        // 验证公司标识不能为空
        RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("公司标识不能为空");

        // 验证名称不能为空
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        // 验证密码不能为空
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("密码不能为空");

        // 验证端口不能为空
        RuleFor(x => x.Port).GreaterThan(0).WithMessage("端口不能为空");

        // 验证服务器不能为空
        RuleFor(x => x.Server).NotNull().NotEmpty().WithMessage("服务器不能为空");

        // 验证用户名不能为空
        RuleFor(x => x.User).NotNull().NotEmpty().WithMessage("用户名不能为空");

        // 调用扩展方法进行额外验证
        OverrideValidator(validate);
    }
}