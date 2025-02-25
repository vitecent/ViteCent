#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
///     Class SearchArgsExtensions.
/// </summary>
public static class BaseSearchArgs
{
    /// <summary>
    ///     Adds
    /// </summary>
    /// <param name="args"></param>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <param name="method">The method.</param>
    /// <param name="group">The group.</param>
    public static void AddArgs(this SearchArgs args, string field, object value, SearchEnum method = SearchEnum.Equal,
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
    ///     Adds the conmpany identifier.
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user">The user.</param>
    /// <param name="field">The field.</param>
    public static void AddConmpanyId(this SearchArgs args, BaseUserInfo user, string field = "CompanyId")
    {
        if (user.IsSuperAdmin == (int)YesNoEnum.No) args.AddArgs(field, user?.Company?.Id ?? default!);
    }

    /// <summary>
    ///     Adds the order.
    /// </summary>
    /// <param name="args"></param>
    /// <param name="field">The field.</param>
    /// <param name="type">The type.</param>
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
    ///     Check arguments company identifier as an asynchronous operation.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <param name="user">The user.</param>
    /// <returns>A Task&lt;BaseResult&gt; representing the asynchronous operation.</returns>
    public static async Task<BaseResult> CheckArgsCompanyIdAsync(string companyId, BaseUserInfo user)
    {
        return await Task.Run(() =>
        {
            if (user.IsSuperAdmin == (int)YesNoEnum.No)
                if (companyId != user.Company.Id)
                    return new BaseResult(401, "您没有权限访问该数据");

            return new BaseResult();
        });
    }
}