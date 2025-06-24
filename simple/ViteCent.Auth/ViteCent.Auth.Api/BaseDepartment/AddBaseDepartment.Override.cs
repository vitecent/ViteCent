/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseDepartment;

/// <summary>
/// </summary>
public partial class AddBaseDepartment
{
    /// <summary>
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <returns>处理结果</returns>
    internal static void OverrideInvoke(AddBaseDepartmentArgs args, BaseUserInfo user)
    {
        args.Status = (int)StatusEnum.Enable;

        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(args.Code) && !string.IsNullOrWhiteSpace(args.Name))
            args.Code = args.Name.GetPinYin().ToCamelCase();
    }
}