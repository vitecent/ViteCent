#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// 搜索用户信息参数
/// </summary>
[Serializable]
public class SearchBaseUserArgs : SearchArgs, IRequest<PageResult<BaseUserResult>>
{
}