#region

using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// SqlSugar数据库操作工厂类，提供数据库连接、事务管理和CRUD等操作功能
/// </summary>
public class SqlSugarFactory : IFactory
{
    /// <summary>
    /// SqlSugar数据库操作客户端实例
    /// </summary>
    private readonly SqlSugarClient client = default!;

    /// <summary>
    /// 日志记录器实例
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// 初始化SqlSugar数据库操作工厂类的新实例
    /// </summary>
    /// <param name="database">数据库连接配置名称</param>
    /// <param name="log">是否启用SQL日志记录，默认为true</param>
    public SqlSugarFactory(string database, bool log = true)
    {
        logger = new BaseLogger(typeof(SqlSugarFactory));

        var configuration = database.GetConfig();

        client = GetSqlSugarClient(configuration, log);
    }

    /// <summary>
    /// </summary>
    /// <param name="configuration">配置信息</param>
    /// <param name="log"></param>
    public SqlSugarFactory(FactoryConfig configuration, bool log = true)
    {
        logger = new BaseLogger(typeof(SqlSugarFactory));

        client = GetSqlSugarClient(configuration, log);
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public async Task BeginTranAsync()
    {
        await client.BeginTranAsync();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public async Task CommitTranAsync()
    {
        await client.CommitTranAsync();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    /// <returns></returns>
    public IDeleteable<T> Delete<T>(List<T> entitys) where T : BaseEntity, new()
    {
        return client.Deleteable(entitys);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public IDeleteable<T> Delete<T>(T entity) where T : BaseEntity, new()
    {
        return client.Deleteable(entity);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IDeleteable<T> Delete<T>() where T : BaseEntity, new()
    {
        return client.Deleteable<T>();
    }

    /// <summary>
    /// 获取快速批量操作接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>返回快速批量操作接口</returns>
    public IFastest<T> Fastest<T>() where T : class, new()
    {
        return client.Fastest<T>();
    }

    /// <summary>
    /// 获取指定数据表的字段信息
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cache">是否使用缓存，默认为false</param>
    /// <returns>返回字段信息列表</returns>
    public async Task<List<BaseFieldInfo>> GetFields(string tableName, bool cache = false)
    {
        var fields = client.DbMaintenance.GetColumnInfosByTableName(tableName, cache);

        var config = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<DbColumnInfo, BaseFieldInfo>()
                .ForMember(x => x.Default, y => y.MapFrom(z => z.DefaultValue))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.ColumnDescription))
                .ForMember(x => x.Identity, y => y.MapFrom(z => z.IsIdentity))
                .ForMember(x => x.PrimaryKey, y => y.MapFrom(z => z.IsPrimarykey))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.DbColumnName))
                .ForMember(x => x.Nullable, y => y.MapFrom(z => z.IsNullable))
                .ForMember(x => x.Type, y => y.MapFrom(z => z.DataType));
        }, new NullLoggerFactory());

        var result = new Mapper(config).Map<List<BaseFieldInfo>>(fields);

        return await Task.FromResult(result);
    }

    /// <summary>
    /// 获取数据库中所有表的信息
    /// </summary>
    /// <param name="cache">是否使用缓存，默认为false</param>
    /// <returns>返回数据表信息列表</returns>
    public async Task<List<BaseTableInfo>> GetTables(bool cache = false)
    {
        var tables = client.DbMaintenance.GetTableInfoList(cache);

        var config = new MapperConfiguration(configuration => { configuration.CreateMap<DbTableInfo, BaseTableInfo>(); }, new NullLoggerFactory());

        var result = new Mapper(config).Map<List<BaseTableInfo>>(tables);

        return await Task.FromResult(result);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public IInsertable<T> Insert<T>(T entity) where T : BaseEntity, new()
    {
        return client.Insertable(entity);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    /// <returns></returns>
    public IInsertable<T> Insert<T>(List<T> entitys) where T : BaseEntity, new()
    {
        return client.Insertable(entitys);
    }

    /// <summary>
    /// 执行分页查询
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="args">查询参数，包含分页、排序等信息</param>
    /// <returns>返回分页查询结果列表</returns>
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
    /// 获取查询接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>返回SqlSugar查询接口</returns>
    public ISugarQueryable<T> Query<T>() where T : class, new()
    {
        return client.Queryable<T>();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public async Task RollbackTranAsync()
    {
        await client.RollbackTranAsync();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public IUpdateable<T> Update<T>(T entity) where T : BaseEntity, new()
    {
        return client.Updateable(entity)
            .IgnoreColumns(ignoreAllNullColumns: true)
            .IsEnableUpdateVersionValidation();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys"></param>
    /// <returns></returns>
    public IUpdateable<T> Update<T>(List<T> entitys) where T : BaseEntity, new()
    {
        return client.Updateable(entitys)
            .IgnoreColumns(ignoreAllNullColumns: true)
            .IsEnableUpdateVersionValidation();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IUpdateable<T> Update<T>() where T : BaseEntity, new()
    {
        return client.Updateable<T>()
            .IgnoreColumns(ignoreAllNullColumns: true)
            .IsEnableUpdateVersionValidation();
    }

    /// <summary>
    /// 根据数据库类型字符串获取对应的DbType枚举值
    /// </summary>
    /// <param name="type">数据库类型字符串</param>
    /// <returns>返回对应的DbType枚举值</returns>
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

    /// <summary>
    /// </summary>
    /// <param name="configuration">配置信息</param>
    /// <param name="log"></param>
    /// <returns>处理结果</returns>
    private SqlSugarClient GetSqlSugarClient(FactoryConfig configuration, bool log = true)
    {
        var slaves = new List<SlaveConnectionConfig>();

        configuration.Slaves.ForEach(slave =>
        {
            slaves.Add(new SlaveConnectionConfig { HitRate = 10, ConnectionString = slave });
        });

        var client = new SqlSugarClient(new ConnectionConfig
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
                var types = new List<System.Data.DbType>
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
                        text = text.Replace(p.ParameterName, p.Value is null ? "" : p.Value.ToString());
                    else
                        text = text.Replace(p.ParameterName, $"'{p.Value ?? default!}'");

                var sql = $"Time: {this.client.Ado.SqlExecutionTime.TotalMilliseconds} ms, SQL:{text}";

                logger.LogInformation(sql);
            };

        return client;
    }
}