/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// 搜索用户信息数据参数
/// </summary>
[Serializable]
public class SearchBaseUserEntityArgs : SearchArgs, IRequest<List<BaseUserEntity>>
{
}