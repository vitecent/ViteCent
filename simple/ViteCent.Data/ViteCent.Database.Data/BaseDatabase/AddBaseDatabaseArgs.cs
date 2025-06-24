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

namespace ViteCent.Database.Data.BaseDatabase;

/// <summary>
/// 新增数据库信息参数
/// </summary>
[Serializable]
public class AddBaseDatabaseArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string CharSet { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 服务器
    /// </summary>
    public string Server { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string User { get; set; } = string.Empty;
}