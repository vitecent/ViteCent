/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// 搜索请假申请模型参数
/// </summary>
[Serializable]
public class SearchUserLeaveEntityArgs : SearchArgs, IRequest<List<UserLeaveEntity>>
{
}