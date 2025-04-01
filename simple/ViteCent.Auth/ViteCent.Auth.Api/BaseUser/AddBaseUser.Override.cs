#region

using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;

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
    private static void OverrideInvoke(AddBaseUserArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.UserNo) && !string.IsNullOrWhiteSpace(args.RealName))
            args.UserNo = args.RealName.GetPinYin();
    }
}