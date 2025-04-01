#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// 搜索调休申请参数
/// </summary>
[Serializable]
public class SearchUserRestArgs : SearchArgs, IRequest<PageResult<UserRestResult>>
{
}