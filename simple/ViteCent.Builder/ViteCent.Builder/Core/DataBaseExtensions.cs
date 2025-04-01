using AutoMapper;
using ViteCent.Builder.Data;
using ViteCent.Core;
using ViteCent.Core.Orm;
using ViteCent.Core.Orm.SqlSugar;

namespace ViteCent.Builder.Core;

/// <summary>
/// </summary>
public class DataBaseExtensions
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static async Task<List<DataBase>> GetDataBase()
    {
        var databases = new List<DataBase>
        {
            new()
            {
                Name = "ViteCent.Auth",
                SmallName = "vitecent_auth",
                ServiceName = "Auth",
                ServicePort = 8000
            },
            new()
            {
                Name = "ViteCent.Basic",
                SmallName = "vitecent_basic",
                ServiceName = "Basic",
                ServicePort = 8010,
                Invoke = true,
                InvokeProjrect="ViteCent.Auth",
                InvokeService="Auth"
            }
        };

        var config = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<BaseTable, Table>();
            configuration.CreateMap<BaseField, Field>();
        });

        foreach (var database in databases)
        {
            var client = new SqlSugarFactory(database.Name);

            var baseTables = await client.GetTables();

            var tables = new Mapper(config).Map<List<Table>>(baseTables);

            database.Tables = tables.OrderBy(x => x.CamelCaseName).ToList();

            foreach (var table in tables)
            {
                table.CamelCaseName = table.Name.ToCamelCase();

                var baseFields = await client.GetFields(table.Name);

                var fields = new Mapper(config).Map<List<Field>>(baseFields);

                foreach (var field in fields)
                {
                    field.CamelCaseName = field.Name.ToCamelCase();
                    field.DataType = field.Type.Replace("varchar", "string")
                        .Replace("nvarchar", "string")
                        .Replace("sql_variant", "string")
                        .Replace("text", "string")
                        .Replace("char", "string")
                        .Replace("ntext", "string")
                        .Replace("hierarchyid", "string")
                        .Replace("bit", "bool")
                        .Replace("datetime", "DateTime")
                        .Replace("datetime2", "DateTime")
                        .Replace("date", "DateTime")
                        .Replace("time", "DateTime")
                        .Replace("smalldatetime", "DateTime")
                        .Replace("DateTimestamp", "DateTime")
                        .Replace("tinyint", "byte")
                        .Replace("bigint", "long")
                        .Replace("longstring", "long")
                        .Replace("single", "decimal")
                        .Replace("money", "decimal")
                        .Replace("numeric", "decimal")
                        .Replace("smallmoney", "decimal")
                        .Replace("float", "decimal")
                        .Replace("real", "float")
                        .Replace("smallint", "short")
                        .Replace("uniqueidentifier", "Guid")
                        .Replace("smallmoney", "decimal");

                    var types = new List<string> { "bool", "DateTime", "long", "decimal", "float", "short" };
                    if (field.Nullable && types.Contains(field.DataType))
                        field.DataType = $"{field.DataType}?";

                    if (field.Length > 0)
                        field.ColumnLength = $", Length = {field.Length}";

                    if (!string.IsNullOrWhiteSpace(field.Type))
                        field.ColumnType = $", ColumnDataType = \"{field.Type}\"";

                    if (!string.IsNullOrWhiteSpace(field.Description))
                        field.ColumnDescription = $", ColumnDescription = \"{field.Description}\"";

                    if (field.PrimaryKey)
                        field.ColumnPrimaryKey = ", IsPrimaryKey = true";

                    if (field.Nullable)
                        field.ColumnNullable = ", IsNullable = true";

                    if (field.Identity)
                        field.ColumnIdentity = ", IsIdentity = true";

                    field.Default = field.DataType == "string" ? " = string.Empty;" : string.Empty;
                }

                table.Fields = [.. fields.OrderBy(x => x.CamelCaseName)];
            }
        }

        return databases;
    }
}