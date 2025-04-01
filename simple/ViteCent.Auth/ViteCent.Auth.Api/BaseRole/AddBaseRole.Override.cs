#region

using ViteCent.Auth.Data.BaseRole;
using ViteCent.Core;

#endregion

namespace ViteCent.Auth.Api.BaseRole;

/// <summary>
/// </summary>
public partial class AddBaseRole
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private static void OverrideInvoke(AddBaseRoleArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}