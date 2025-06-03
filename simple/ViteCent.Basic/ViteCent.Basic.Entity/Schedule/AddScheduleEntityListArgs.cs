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

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// 批量新增排班信息模型
/// </summary>
[Serializable]
public class AddScheduleEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 排班信息
    /// </summary>
    public List<AddScheduleEntity> Items = [];
}