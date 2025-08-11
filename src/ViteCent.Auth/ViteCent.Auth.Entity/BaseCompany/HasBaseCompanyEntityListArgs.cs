/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入MediatR命名空间，用于实现CQRS模式的请求处理
using MediatR;

// 引入ViteCent.Core.Data命名空间，用于使用基础数据类型和结果类型
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 批量公司信息判重参数实体类
/// </summary>
/// <remarks>该类用于批量检查公司信息是否存在重复数据 继承自BaseArgs基类，实现IRequest接口以支持MediatR中介者模式 返回类型为BaseResult，用于标识判重结果</remarks>
[Serializable] // 标记类可序列化，支持对象的序列化和反序列化操作
public class HasBaseCompanyEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司编码列表
    /// </summary>
    /// <remarks>用于批量检查公司编码是否存在重复 列表中的每个编码都将被用于查重判断</remarks>
    public List<string> Codes { get; set; } = []; // 使用C# 12的集合表达式语法初始化空列表

    /// <summary>
    /// 公司名称列表
    /// </summary>
    /// <remarks>用于批量检查公司名称是否存在重复 列表中的每个名称都将被用于查重判断</remarks>
    public List<string> Names { get; set; } = []; // 使用C# 12的集合表达式语法初始化空列表
}