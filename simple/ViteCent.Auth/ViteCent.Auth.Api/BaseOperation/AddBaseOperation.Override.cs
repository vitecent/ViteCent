#region

using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Core;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseOperation;

/// <summary>
/// </summary>
public partial class AddBaseOperation
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private void OverrideInvoke(AddBaseOperationArgs args)
    {
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = User.Company.Id;

        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}