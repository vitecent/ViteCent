#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// 搜索请假申请参数
/// </summary>
[Serializable]
public class SearchUserLeaveArgs : SearchArgs, IRequest<PageResult<UserLeaveResult>>
{
}