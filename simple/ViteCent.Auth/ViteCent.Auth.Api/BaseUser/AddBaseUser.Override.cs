/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// </summary>
public partial class AddBaseUser
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(AddBaseUserArgs args, BaseUserInfo user)
    {
        args.Status = (int)StatusEnum.Enable;
        args.IsSuper = (int)YesNoEnum.No;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = user.Company.Id;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = user.Department.Id;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.PositionId))
                args.PositionId = user.Position.Id;

        if (string.IsNullOrEmpty(args.Username) && !string.IsNullOrWhiteSpace(args.RealName))
            args.Username = args.RealName.GetPinYin().ToCamelCase();

        if (string.IsNullOrEmpty(args.Password))
            args.Password = BaseConst.DefaultPassword;
    }
}