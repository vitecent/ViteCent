#region

using System.Text;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// 搜索参数基类，提供通用的查询条件、分页和排序功能
/// </summary>
public class SearchArgs : BaseArgs
{
    /// <summary>
    /// 参数索引，用于生成SQL参数名称
    /// </summary>
    private int index = 1;

    /// <summary>
    /// 查询条件列表，包含字段名、查询方法和查询值
    /// </summary>
    public List<SearchItem> Args { get; set; } = [];

    /// <summary>
    /// 每页记录数，用于分页查询
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    /// 当前页码，用于分页查询的偏移量
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// 排序字段列表，定义查询结果的排序规则
    /// </summary>
    public List<OrderField> Order { get; set; } = [];

    /// <summary>
    /// 查询结果的总记录数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 将查询条件转换为SQL语句和参数
    /// </summary>
    /// <returns>返回元组，包含SQL WHERE子句和对应的参数字典</returns>
    public (string, object) ToSql()
    {
        var result = string.Empty;
        var parameters = new Dictionary<string, object>();

        //删除空
        Args.RemoveAll(x =>
            string.IsNullOrWhiteSpace(x.Field) || string.IsNullOrWhiteSpace(x.Value) || x.Value == "[]");

        if (Args.Count == 0) return (result, parameters);

        var sql = new StringBuilder();
        sql.Append("1 = 1 ");

        foreach (var item in Args)
            if (item.Value.StartsWith('[') && item.Value.EndsWith(']'))
            {
                var values = item.Value.DeJson<List<string>>();

                if (values.Count > 0)
                {
                    var value = string.Join(",", values.Select(x => $"'{x}'").ToArray());

                    item.Value = value;
                }
            }

        //处理普通
        var list = Args.Where(x => string.IsNullOrWhiteSpace(x.Group)).ToList();

        var i = 0;

        list.ForEach(x =>
        {
            sql.Append($"AND {ToSql(x, parameters)}");
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

                    sql.Append(ToSql(x, parameters));

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
                            sql.Append(ToSql(g.First(), parameters));
                        }
                        else
                        {
                            sql.Append("(" +
                                       "");
                            var j = 0;
                            g.ToList().ForEach(x =>
                            {
                                if (j != 0) sql.Append("AND ");
                                sql.Append(ToSql(x, parameters));
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
    /// 将单个查询条件转换为SQL语句片段
    /// </summary>
    /// <param name="item">查询条件项，包含字段名、查询方法和查询值</param>
    /// <param name="parameters">SQL参数字典，用于存储参数值</param>
    /// <returns>返回SQL语句片段</returns>
    private string ToSql(SearchItem item, Dictionary<string, object> parameters)
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
                sql = $"{item.Field} IN ({item.Value}) ";
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
                var keys = item.Value?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

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
                var keys = item.Value?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                keys?.ForEach(key =>
                {
                    parameters.Add($"{item.Field}{index}", $"%{key}%");
                    index++;
                });
            }
            else
            {
                if (item.Method == SearchEnum.Like || item.Method == SearchEnum.NoLike)
                {
                    parameters.Add($"{item.Field}{index}", $"%{item.Value}%");
                }
                else if (item.Method == SearchEnum.LikeLeft)
                {
                    parameters.Add($"{item.Field}{index}", $"%{item.Value}");
                }
                else if (item.Method == SearchEnum.LikeRight)
                {
                    parameters.Add($"{item.Field}{index}", $"{item.Value}%");
                }
                else if (item.Method == SearchEnum.In)
                {
                }
                else
                {
                    parameters.Add($"{item.Field}{index}", item.Value ?? default!);
                }

                index++;
            }
        }

        return sql;
    }
}