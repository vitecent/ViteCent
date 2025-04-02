/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Core;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseCompany;

/// <summary>
/// </summary>
public partial class AddBaseCompany
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private static void OverrideInvoke(AddBaseCompanyArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}