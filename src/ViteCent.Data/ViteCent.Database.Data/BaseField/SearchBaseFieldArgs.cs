/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入核心数据类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Database.Data.BaseField;

/// <summary>
/// 搜索表字段信息参数
/// </summary>
[Serializable]
public class SearchBaseFieldArgs : SearchArgs, IRequest<PageResult<BaseFieldResult>>
{
}