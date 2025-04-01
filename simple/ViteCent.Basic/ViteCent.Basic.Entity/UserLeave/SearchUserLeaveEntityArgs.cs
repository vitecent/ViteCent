#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// 搜索请假申请数据参数
/// </summary>
[Serializable]
public class SearchUserLeaveEntityArgs : SearchArgs, IRequest<List<UserLeaveEntity>>
{
}