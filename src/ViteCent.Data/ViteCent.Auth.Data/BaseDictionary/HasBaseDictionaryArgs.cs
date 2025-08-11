/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseDictionary;

/// <summary>
/// 字典信息判重参数
/// </summary>
[Serializable]
public class HasBaseDictionaryEntityArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;
}