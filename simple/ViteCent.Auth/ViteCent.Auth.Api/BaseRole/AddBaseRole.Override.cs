/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseRole;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseRole;

/// <summary>
/// </summary>
public partial class AddBaseRole
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(AddBaseRoleArgs args, BaseUserInfo user)
    {
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = user.Company.Id;

        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}