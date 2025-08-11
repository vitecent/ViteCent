/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Database.Entity.BaseField;

/// <summary>
/// 批量表字段信息判重模型参数
/// </summary>
[Serializable]
public class HasBaseFieldEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public List<string> CompanyIds { get; set; } = [];
}