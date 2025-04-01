#region

using ViteCent.Auth.Data.BaseResource;
using ViteCent.Core;

#endregion

namespace ViteCent.Auth.Api.BaseResource;

/// <summary>
/// </summary>
public partial class AddBaseResource
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private static void OverrideInvoke(AddBaseResourceArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}