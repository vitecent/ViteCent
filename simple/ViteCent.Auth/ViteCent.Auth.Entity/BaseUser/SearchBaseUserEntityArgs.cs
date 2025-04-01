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