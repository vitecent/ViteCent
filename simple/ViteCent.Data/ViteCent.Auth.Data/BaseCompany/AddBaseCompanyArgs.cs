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

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// 新增公司信息参数
/// </summary>
[Serializable]
public class AddBaseCompanyArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// 国家
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishDate { get; set; }

    /// <summary>
    /// 行业
    /// </summary>
    public string? Industry { get; set; }

    /// <summary>
    /// 法人
    /// </summary>
    public string? LegalPerson { get; set; }

    /// <summary>
    /// 法人电话
    /// </summary>
    public string? LegalPhone { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public string? Level { get; set; }

    /// <summary>
    /// 商标
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 父级标识
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// 省份
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
}