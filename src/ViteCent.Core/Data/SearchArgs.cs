#region

using System.Text;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
///     Class SearchArgs. Implements the <see cref="ViteCent.Core.Data.BaseArgs" />
/// </summary>
/// <seealso cref="ViteCent.Core.Data.BaseArgs" />
public class SearchArgs : BaseArgs
{
    /// <summary>
    ///     The index
    /// </summary>
    private int index = 1;

    /// <summary>
    ///     Gets or sets
    /// </summary>
    /// <value></value>
    public List<SearchItem> Args { get; set; } = [];

    /// <summary>
    ///     Gets or sets the limit.
    /// </summary>
    /// <value>The limit.</value>
    public int Limit { get; set; }

    /// <summary>
    ///     Gets or sets the offset.
    /// </summary>
    /// <value>The offset.</value>
    public int Offset { get; set; }

    /// <summary>
    ///     Gets or sets the order.
    /// </summary>
    /// <value>The order.</value>
    public List<OrderField> Order { get; set; } = [];

    /// <summary>
    ///     Gets or sets the total.
    /// </summary>
    /// <value>The total.</value>
    public int Total { get; set; }

    /// <summary>
    ///     Converts to sql.
    /// </summary>
    /// <returns>(string, object).</returns>
    public (string, object) ToSql()
    {
        var result = string.Empty;
        var parameters = new Dictionary<string, object>();

        if (Args.Count == 0) return (result, parameters);

        //删除空
        Args.RemoveAll(x =>
            string.IsNullOrWhiteSpace(x.Field) || string.IsNullOrWhiteSpace(x.Value?.ToString()) ||
            x.Value?.ToString() == "[]");

        var sql = new StringBuilder();
        sql.Append("1 = 1 ");

        //处理普通
        var list = Args.Where(x => string.IsNullOrWhiteSpace(x.Group)).ToList();
        var i = 0;

        list.ForEach(x =>
        {
            sql.Append($"AND {ToSQL(x, parameters)}");
            i++;
        });

        //处理普通分组
        list = Args.Where(x => !string.IsNullOrWhiteSpace(x.Group) && x.Group.LastIndexOf(',') == -1).ToList();

        if (list.Count > 1)
        {
            var group = list.GroupBy(x => x.Group).ToList();
            group.ForEach(g =>
            {
                sql.Append("AND (");
                i = 0;

                g.ToList().ForEach(x =>
                {
                    if (i != 0) sql.Append("OR ");

                    sql.Append(ToSQL(x, parameters));

                    i++;
                });

                sql.Append(") ");
            });
        }

        //处理复杂分组
        list = Args.Where(x => !string.IsNullOrWhiteSpace(x.Group) && x.Group.LastIndexOf(',') != -1).ToList();

        if (list.Count > 1)
        {
            var keys = list.Select(x => x.Group.Split(',', StringSplitOptions.RemoveEmptyEntries)[0]).Distinct()
                .ToList();

            keys.ForEach(key =>
            {
                var groups = list.Where(x => x.Group.StartsWith($"{key},")).ToList();
                var group = groups.GroupBy(x => x.Group).ToList();

                if (group.Count > 1)
                {
                    sql.Append("AND (");
                    i = 0;
                    group.ForEach(g =>
                    {
                        if (i != 0) sql.Append("OR ");

                        if (g.Count() == 1)
                        {
                            sql.Append(ToSQL(g.First(), parameters));
                        }
                        else
                        {
                            sql.Append('(');
                            var j = 0;
                            g.ToList().ForEach(x =>
                            {
                                if (j != 0) sql.Append("AND ");
                                sql.Append(ToSQL(x, parameters));
                                j++;
                            });
                            sql.Append(") ");
                        }

                        i++;
                    });

                    sql.Append(") ");
                }
            });
        }

        result = sql.ToString();

        return (result, parameters);
    }

    /// <summary>
    ///     Converts to sql.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>string.</returns>
    private string ToSQL(SearchItem item, Dictionary<string, object> parameters)
    {
        var sql = string.Empty;
        var flag = true;

        switch (item.Method)
        {
            case SearchEnum.Equal:
                sql = $"{item.Field} = @{item.Field}{index} ";
                break;

            case SearchEnum.Like or SearchEnum.LikeLeft or SearchEnum.LikeRight:
                sql = $"{item.Field} LIKE @{item.Field}{index} ";
                break;

            case SearchEnum.GreaterThan:
                sql = $"{item.Field} > @{item.Field}{index} ";
                break;

            case SearchEnum.GreaterThanOrEqual:
                sql = $"{item.Field} >= @{item.Field}{index} ";
                break;

            case SearchEnum.LessThan:
                sql = $"{item.Field} < @{item.Field}{index} ";
                break;

            case SearchEnum.LessThanOrEqual:
                sql = $"{item.Field} <= @{item.Field}{index} ";
                break;

            case SearchEnum.In:
                sql = $"{item.Field} IN (@{item.Field}{index}) ";
                break;

            case SearchEnum.NotIn:
                sql = $"{item.Field} NOT IN (@{item.Field}{index}) ";
                break;

            case SearchEnum.NoEqual:
                sql = $"{item.Field} <> @{item.Field}{index} ";
                break;

            case SearchEnum.IsNullOrEmpty:
                sql = $"{item.Field} = '' ";
                flag = false;
                break;

            case SearchEnum.IsNot:
                sql = $"{item.Field} IS NOT NULL ";
                flag = false;
                break;

            case SearchEnum.NoLike:
                sql = $"{item.Field} NOT LIKE @{item.Field}{index} ";
                break;

            case SearchEnum.EqualNull:
                sql = $"{item.Field} IS NULL ";
                break;

            case SearchEnum.InLike:
                var keys = item.Value?.ToString()?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (keys?.Count > 1)
                {
                    var i = 0;
                    var temp = new StringBuilder();
                    temp.Append('(');
                    keys.ForEach(key =>
                    {
                        if (i != 0) temp.Append("OR ");
                        temp.Append($"{item.Field} LIKE @{item.Field}{index} ");
                        index++;
                        i++;
                    });
                    temp.Append(") ");

                    sql = temp.ToString();
                    index -= i;
                }

                break;
        }

        if (flag)
        {
            if (item.Method == SearchEnum.InLike)
            {
                var keys = item.Value?.ToString()?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                keys?.ForEach(key =>
                {
                    parameters.Add($"{item.Field}{index}", $"%{key}%");
                    index++;
                });
            }
            else
            {
                if (item.Method == SearchEnum.Like || item.Method == SearchEnum.NoLike)
                    parameters.Add($"{item.Field}{index}", $"%{item.Value}%");
                else if (item.Method == SearchEnum.LikeLeft)
                    parameters.Add($"{item.Field}{index}", $"%{item.Value}");
                else if (item.Method == SearchEnum.LikeRight)
                    parameters.Add($"{item.Field}{index}", $"{item.Value}%");
                else
                    parameters.Add($"{item.Field}{index}", item.Value ?? default!);
                index++;
            }
        }

        return sql;
    }
}