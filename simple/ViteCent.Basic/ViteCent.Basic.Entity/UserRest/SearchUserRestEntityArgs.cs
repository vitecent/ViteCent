#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// 搜索调休申请数据参数
/// </summary>
[Serializable]
public class SearchUserRestEntityArgs : SearchArgs, IRequest<List<UserRestEntity>>
{
}