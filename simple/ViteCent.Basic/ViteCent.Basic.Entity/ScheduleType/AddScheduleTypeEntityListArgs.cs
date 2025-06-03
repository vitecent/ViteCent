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

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// 批量新增基础排班模型
/// </summary>
[Serializable]
public class AddScheduleTypeEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 基础排班
    /// </summary>
    public List<AddScheduleTypeEntity> Items = [];
}