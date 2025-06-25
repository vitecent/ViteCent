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
/// 新增表字段信息参数
/// </summary>
[Serializable]
public class AddBaseFieldArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 新增是否可见
    /// </summary>
    public byte? Add { get; set; }

    /// <summary>
    /// 新增排序
    /// </summary>
    public int? AddSort { get; set; }

    /// <summary>
    /// 新增宽度
    /// </summary>
    public int? AddWidth { get; set; }

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
    /// 详情是否可见
    /// </summary>
    public byte? Detail { get; set; }

    /// <summary>
    /// 详情排序
    /// </summary>
    public int? DetailSort { get; set; }

    /// <summary>
    /// 详情宽度
    /// </summary>
    public int? DetailWidth { get; set; }

    /// <summary>
    /// 编辑是否可见
    /// </summary>
    public byte? Edit { get; set; }

    /// <summary>
    /// 编辑排序
    /// </summary>
    public int? EditSort { get; set; }

    /// <summary>
    /// 编辑宽度
    /// </summary>
    public int? EditWidth { get; set; }

    /// <summary>
    /// 导出是否可见
    /// </summary>
    public byte? Export { get; set; }

    /// <summary>
    /// 导出排序
    /// </summary>
    public int? ExportSort { get; set; }

    /// <summary>
    /// 导出宽度
    /// </summary>
    public int? ExportWidth { get; set; }

    /// <summary>
    /// 是否自增
    /// </summary>
    public byte? Identity { get; set; }

    /// <summary>
    /// 导入是否可见
    /// </summary>
    public byte? Import { get; set; }

    /// <summary>
    /// 导入排序
    /// </summary>
    public int? ImportSort { get; set; }

    /// <summary>
    /// 导入宽度
    /// </summary>
    public int? ImportWidth { get; set; }

    /// <summary>
    /// 是否索引
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 下拉是否可见
    /// </summary>
    public byte? List { get; set; }

    /// <summary>
    /// 下拉排序
    /// </summary>
    public int? ListSort { get; set; }

    /// <summary>
    /// 下拉宽度
    /// </summary>
    public int? ListWidth { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否主键
    /// </summary>
    public byte? PrimaryKey { get; set; }

    /// <summary>
    /// 打印是否可见
    /// </summary>
    public byte? Print { get; set; }

    /// <summary>
    /// 打印排序
    /// </summary>
    public int? PrintSort { get; set; }

    /// <summary>
    /// 打印宽度
    /// </summary>
    public int? PrintWidth { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 是否分表字段
    /// </summary>
    public byte? SplitField { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 列表是否可见
    /// </summary>
    public byte? Table { get; set; }

    /// <summary>
    /// 列表排序
    /// </summary>
    public int? TableSort { get; set; }

    /// <summary>
    /// 列表宽度
    /// </summary>
    public int? TableWidth { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 是否唯一
    /// </summary>
    public byte? Unique { get; set; }

    /// <summary>
    /// 是否版本字段
    /// </summary>
    public byte? VersionField { get; set; }
}