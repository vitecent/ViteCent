/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api.BaseRolePermission;

/// <summary>
/// </summary>
public partial class AddBaseRolePermission
{
    /// <summary>
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <returns>处理结果</returns>
    internal static void OverrideInvoke(AddBaseRolePermissionArgs args, BaseUserInfo user)
    {
        args.Status = (int)StatusEnum.Enable;

        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;
    }
}