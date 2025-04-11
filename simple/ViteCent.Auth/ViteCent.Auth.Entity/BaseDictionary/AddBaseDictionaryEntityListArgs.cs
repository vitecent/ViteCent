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

namespace ViteCent.Auth.Entity.BaseDictionary;

/// <summary>
/// 批量新增字典信息参数
/// </summary>
[Serializable]
public class AddBaseDictionaryEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public List<AddBaseDictionaryEntity> Items = [];
}