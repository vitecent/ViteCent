#region

using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core;

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
    private static void OverrideInvoke(AddBaseSystemArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}