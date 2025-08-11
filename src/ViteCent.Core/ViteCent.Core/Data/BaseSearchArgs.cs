#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// 搜索参数基础工具类，提供添加查询条件、排序和权限验证等功能
/// </summary>
public static class BaseSearchArgs
{
    /// <summary>
    /// 添加查询条件
    /// </summary>
    /// <param name="args">搜索参数对象</param>
    /// <param name="field">查询字段名</param>
    /// <param name="value">查询值</param>
    /// <param name="method">查询方式，默认为等于</param>
    /// <param name="group">条件分组，默认为空</param>
    public static void AddArgs(this SearchArgs args, string field, string value, SearchEnum method = SearchEnum.Equal,
        string group = "")
    {
        if (args.Args.Any(x => x.Field == field)) args.Args.RemoveAll(x => x.Field == field);

        args.Args.Add(new SearchItem
        {
            Field = field,
            Method = method,
            Value = value,
            Group = group
        });
    }

    /// <summary>
    /// 添加公司标识查询条件，用于数据权限控制
    /// </summary>
    /// <param name="args">搜索参数对象</param>
    /// <param name="user">当前用户信息</param>
    /// <param name="field">公司标识字段名，默认为CompanyId</param>
    public static void AddCompanyId(this SearchArgs args, BaseUserInfo user, string field = "CompanyId")
    {
        if (user.IsSuper != (int)YesNoEnum.Yes)
            args.AddArgs(field, user?.Company?.Id ?? default!);
    }

    /// <summary>
    /// 添加排序条件
    /// </summary>
    /// <param name="args">搜索参数对象</param>
    /// <param name="field">排序字段名</param>
    /// <param name="type">排序方式，默认为降序</param>
    public static void AddOrder(this SearchArgs args, string field, OrderEnum type = OrderEnum.Desc)
    {
        if (args.Order.Any(x => x.Field == field)) args.Args.RemoveAll(x => x.Field == field);

        args.Order.Add(new OrderField
        {
            Field = field,
            OrderType = type
        });
    }

    /// <summary>
    /// 检查用户是否有权限访问指定公司的数据
    /// </summary>
    /// <param name="user">当前用户信息</param>
    /// <param name="companyId">要访问的公司标识</param>
    /// <returns>权限检查结果，成功返回空消息，失败返回错误信息</returns>
    public static BaseResult CheckCompanyId(this BaseUserInfo user, string companyId)
    {
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (companyId != user.Company.Id)
                return new BaseResult(401, "您没有权限访问该数据");

        return new BaseResult();
    }
}