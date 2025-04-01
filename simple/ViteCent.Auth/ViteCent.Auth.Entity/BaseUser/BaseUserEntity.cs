#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_user")]
public class BaseUserEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "avatar", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "头像")]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "birthday", IsNullable = true, ColumnDataType = "date", ColumnDescription = "出生日期")]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "color", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "颜色")]
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "companyId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "公司标识")]
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "createTime", IsNullable = true, ColumnDataType = "datetime", ColumnDescription = "创建时间")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "creator", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "创建人")]
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "dataVersion", ColumnDataType = "timestamp", ColumnDescription = "数据版本")]
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "departmentId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "部门标识")]
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "description", IsNullable = true, ColumnDataType = "varchar", Length = 5000, ColumnDescription = "简介")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "email", IsNullable = true, ColumnDataType = "varchar", Length = 100, ColumnDescription = "邮箱")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "gender", IsNullable = true, ColumnDataType = "int", Length = 11, ColumnDescription = "性别")]
    public int Gender { get; set; }

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, ColumnDescription = "标识", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "idCard", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "身份证")]
    public string IdCard { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "nickname", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "昵称")]
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "password", ColumnDataType = "varchar", Length = 50, ColumnDescription = "密码")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "phone", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "电话")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "positionId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "职位标识")]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "realName", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "真实姓名")]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "status", IsNullable = true, ColumnDataType = "int", Length = 11, ColumnDescription = "状态")]
    public int Status { get; set; }

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "updater", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "修改人")]
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "updateTime", IsNullable = true, ColumnDataType = "datetime", ColumnDescription = "修改时间")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "username", ColumnDataType = "varchar", Length = 50, ColumnDescription = "用户名")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    [SugarColumn(ColumnName = "userNo", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "编号")]
    public string UserNo { get; set; } = string.Empty;
}