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

namespace ViteCent.Auth.Entity.BaseOperation;

/// <summary>
/// 批量新增操作信息参数
/// </summary>
[Serializable]
public class AddBaseOperationEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 操作信息
    /// </summary>
    public List<AddBaseOperationEntity> Items = [];
}