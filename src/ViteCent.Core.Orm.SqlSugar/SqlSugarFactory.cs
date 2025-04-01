#region

using AutoMapper;
using log4net;
using SqlSugar;
using System.Linq.Expressions;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// </summary>
public class SqlSugarFactory : IFactory
{
    /// <summary>
    /// </summary>
    private readonly SqlSugarClient client = default!;

    /// <summary>
    /// </summary>
    private readonly List<Command> commands = [];

    /// <summary>
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// </summary>
    /// <param name="dataBase"></param>
    /// <param name="log"></param>
    public SqlSugarFactory(string dataBase, bool log = true)
    {
        logger = new BaseLogger(typeof(SqlSugarFactory));

        var configuration = FactoryConfigExtensions.GetConfig(dataBase);

        var slaves = new List<SlaveConnectionConfig>();

        configuration.Slaves.ForEach(slave =>
        {
            slaves.Add(new SlaveConnectionConfig { HitRate = 10, ConnectionString = slave });
        });

        client = new SqlSugarClient(new ConnectionConfig
        {
            ConnectionString = configuration.Master,
            SlaveConnectionConfigs = slaves,
            DbType = GetDbType(configuration.DbType),
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true
        });

        client.Ado.CommandTimeOut = 30;

        if (log)
            client.Aop.OnLogExecuted = (text, parameter) =>
            {
                var types = new List<System.Data.DbType>()
                {
                     System.Data.DbType.Int16,
                     System.Data.DbType.Int32,
                     System.Data.DbType.Int64,
                     System.Data.DbType.Decimal,
                     System.Data.DbType.Double,
                     System.Data.DbType.Single,
                     System.Data.DbType.UInt16,
                     System.Data.DbType.UInt32,
                     System.Data.DbType.UInt64,
                     System.Data.DbType.VarNumeric
                };

                foreach (var p in parameter)
                    if (types.Contains(p.DbType))
                        text = text.Replace(p.ParameterName, p.Value == null ? "" : p.Value.ToString());
                    else
                        text = text.Replace(p.ParameterName, $"'{p.Value ?? default!}'");

                var sql = $"Time: {client.Ado.SqlExecutionTime.TotalMilliseconds} ms, SQL:{text}";

                logger.LogInformation(sql);
            };
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResult> CommitAsync()
    {
        if (commands.Count <= 0) return new BaseResult(string.Empty);

        client.BeginTran();
        try
        {
            commands.ForEach(x =>
            {
                if (x.DataType == DataEnum.SQL)
                    client.Ado.ExecuteCommand(x.SQL, x.Parameters);
                else
                    switch (x.CommandType)
                    {
                        case CommandEnum.Insert:
                            client.Insertable(x.Entity).ExecuteCommand();
                            break;

                        case CommandEnum.Update:
                            if (x.DataType == DataEnum.Entity)
                                client.Updateable(x.Entity).IgnoreColumns(true)
                                    .IsEnableUpdateVersionValidation().ExecuteCommand();
                            else
                                client.Updateable(x.Entity).UpdateColumns(x.Where)
                                    .IgnoreColumns(ignoreAllNullColumns: true).IsEnableUpdateVersionValidation()
                                    .ExecuteCommand();
                            break;

                        case CommandEnum.Delete:
                            if (x.DataType == DataEnum.Entity)
                                client.Deleteable(x.Entity).ExecuteCommand();
                            else
                                client.Deleteable(x.Where).ExecuteCommand();
                            break;
                    }
            });

            client.CommitTran();

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            client.RollbackTran();
            logger.LogError(e, e.Message);
            return new BaseResult(500, e.Message);
        }

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="where"></param>
    public void Delete<T>(Expression<Func<T, bool>> where) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Delete, DataType = DataEnum.Where, Where = where });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Delete<T>(T entity) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Delete, DataType = DataEnum.Entity, Entity = entity });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    public void Delete<T>(List<T> entitys) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Delete, DataType = DataEnum.Entity, Entity = entitys });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    public void Delete<T>(string sql, object parameters = default!) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Delete, DataType = DataEnum.SQL, SQL = sql, Parameters = parameters });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IFastest<T> Fastest<T>() where T : class, new()
    {
        return client.Fastest<T>();
    }

    /// <summary>
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public async Task<List<BaseField>> GetFields(string tableName, bool cache = false)
    {
        var fields = client.DbMaintenance.GetColumnInfosByTableName(tableName, cache);

        var config = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<DbColumnInfo, BaseField>()
                .ForMember(x => x.Default, y => y.MapFrom(z => z.DefaultValue))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.ColumnDescription))
                .ForMember(x => x.Identity, y => y.MapFrom(z => z.IsIdentity))
                .ForMember(x => x.PrimaryKey, y => y.MapFrom(z => z.IsPrimarykey))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.DbColumnName))
                .ForMember(x => x.Nullable, y => y.MapFrom(z => z.IsNullable))
                .ForMember(x => x.Type, y => y.MapFrom(z => z.DataType));
        });

        var result = new Mapper(config).Map<List<BaseField>>(fields);

        return await Task.FromResult(result);
    }

    /// <summary>
    /// </summary>
    /// <param name="cache"></param>
    /// <returns></returns>
    public async Task<List<BaseTable>> GetTables(bool cache = false)
    {
        var tables = client.DbMaintenance.GetTableInfoList(cache);

        var config = new MapperConfiguration(configuration => { configuration.CreateMap<DbTableInfo, BaseTable>(); });

        var result = new Mapper(config).Map<List<BaseTable>>(tables);

        return await Task.FromResult(result);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Insert<T>(T entity) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Insert, DataType = DataEnum.Entity, Entity = entity });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    public void Insert<T>(List<T> entitys) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Insert, DataType = DataEnum.Entity, Entity = entitys });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    public void Insert<T>(string sql, object parameters = default!) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Insert, DataType = DataEnum.SQL, SQL = sql, Parameters = parameters });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<List<T>> PageAsync<T>(SearchArgs args) where T : class, new()
    {
        if (args.Offset < 1) args.Offset = 1;

        if (args.Limit < 1) args.Limit = 10;

        var where = args.ToSql();
        var query = client.Queryable<T>().Where(where.Item1, where.Item2);

        foreach (var order in args.Order)
            if (order.OrderType == OrderEnum.Asc)
                query = query.OrderByPropertyName(order.Field, OrderByType.Asc);
            else
                query = query.OrderByPropertyName(order.Field, OrderByType.Desc);

        RefAsync<int> total = 0;
        var list = await query.ToPageListAsync(args.Offset, args.Limit, total);
        args.Total = total;

        return list;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ISugarQueryable<T> Query<T>() where T : class, new()
    {
        return client.Queryable<T>();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Update<T>(T entity) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Update, DataType = DataEnum.Entity, Entity = entity });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="columns"></param>
    public void Update<T>(T entity, Expression<Func<T, object>> columns) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Update, DataType = DataEnum.Where, Entity = entity, Where = columns });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    public void Update<T>(List<T> entitys) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Update, DataType = DataEnum.Entity, Entity = entitys });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    /// <param name="columns"></param>
    public void Update<T>(List<T> entitys, Expression<Func<T, object>> columns) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Update, DataType = DataEnum.Where, Entity = entitys, Where = columns });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    public void Update<T>(string sql, object parameters = default!) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Update, DataType = DataEnum.SQL, SQL = sql, Parameters = parameters });
    }

    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static DbType GetDbType(string type)
    {
        return type switch
        {
            "MySql" => DbType.MySql,
            "SqlServer" => DbType.SqlServer,
            "Sqlite" => DbType.Sqlite,
            "Oracle" => DbType.Oracle,
            "PostgreSQL" => DbType.PostgreSQL,
            "Dm" => DbType.Dm,
            "Kdbndp" => DbType.Kdbndp,
            "Oscar" => DbType.Oscar,
            "MySqlConnector" => DbType.MySqlConnector,
            "Access" => DbType.Access,
            "OpenGauss" => DbType.OpenGauss,
            "QuestDB" => DbType.QuestDB,
            "HG" => DbType.HG,
            "ClickHouse" => DbType.ClickHouse,
            "GBase" => DbType.GBase,
            "Odbc" => DbType.Odbc,
            "OceanBaseForOracle" => DbType.OceanBaseForOracle,
            "TDengine" => DbType.TDengine,
            "GaussDB" => DbType.GaussDB,
            "OceanBase" => DbType.OceanBase,
            "Tidb" => DbType.Tidb,
            "Vastbase" => DbType.Vastbase,
            "PolarDB" => DbType.PolarDB,
            "Doris" => DbType.Doris,
            "Xugu" => DbType.Xugu,
            "GoldenDB" => DbType.GoldenDB,
            "TDSQLForPGODBC" => DbType.TDSQLForPGODBC,
            "TDSQL" => DbType.TDSQL,
            "HANA" => DbType.HANA,
            _ => DbType.Custom
        };
    }
}