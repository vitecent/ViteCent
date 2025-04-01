#region

using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseSystem;

/// <summary>
/// </summary>
public partial class AddBaseSystem
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private void OverrideInvoke(AddBaseSystemArgs args)
    {
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = User.Company.Id;

        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}