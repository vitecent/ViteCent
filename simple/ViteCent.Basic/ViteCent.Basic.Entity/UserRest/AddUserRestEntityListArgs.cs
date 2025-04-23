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

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// 批量新增调休申请参数
/// </summary>
[Serializable]
public class AddUserRestEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 调休申请
    /// </summary>
    public List<AddUserRestEntity> Items = [];
}