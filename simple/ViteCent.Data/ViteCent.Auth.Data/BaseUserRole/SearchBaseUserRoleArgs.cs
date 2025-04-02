/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// 搜索用户角色参数
/// </summary>
[Serializable]
public class SearchBaseUserRoleArgs : SearchArgs, IRequest<PageResult<BaseUserRoleResult>>
{
}