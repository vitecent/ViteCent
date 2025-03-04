#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public static class BaseSearchArgs
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="method"></param>
    /// <param name="group"></param>
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
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <param name="field"></param>
    public static void AddConmpanyId(this SearchArgs args, BaseUserInfo user, string field = "CompanyId")
    {
        if (user.IsSuper != (int)YesNoEnum.Yes) args.AddArgs(field, user?.Company?.Id ?? default!);
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="field"></param>
    /// <param name="type"></param>
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
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckCompanyIdArgsAsync(string companyId, BaseUserInfo user)
    {
        return await Task.Run(() =>
        {
            if (user.IsSuper != (int)YesNoEnum.Yes)
                if (companyId != user.Company.Id)
                    return new BaseResult(401, "您没有权限访问该数据");

            return new BaseResult();
        });
    }
}