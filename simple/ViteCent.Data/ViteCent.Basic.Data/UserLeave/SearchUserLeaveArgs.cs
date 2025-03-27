#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// </summary>
[Serializable]
public class SearchUserLeaveArgs : SearchArgs, IRequest<PageResult<UserLeaveResult>>
{
}