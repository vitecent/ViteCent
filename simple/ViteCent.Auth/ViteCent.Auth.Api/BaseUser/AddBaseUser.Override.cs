/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Auth.Data.BaseUser;
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
    /// <returns></returns>
    private void OverrideInvoke(AddBaseUserArgs args)
    {
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = User.Company.Id;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = User.Department.Id;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.PositionId))
                args.PositionId = User.Position.Id;
    }
}