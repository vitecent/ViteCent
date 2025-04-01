#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// </summary>
[Serializable]
public class SearchUserLeaveEntityArgs : SearchArgs, IRequest<List<UserLeaveEntity>>
{
}