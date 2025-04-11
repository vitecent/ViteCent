/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// 批量新增请假申请参数
/// </summary>
[Serializable]
public class AddUserLeaveEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 请假申请
    /// </summary>
    public List<AddUserLeaveEntity> Items = [];
}