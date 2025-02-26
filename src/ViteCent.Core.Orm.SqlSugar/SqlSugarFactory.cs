#region

using AutoMapper;

using SqlSugar;
using System.Linq.Expressions;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
///     Class SqlSugarFactory. Implements the <see cref="ViteCent.Core.Orm.IFactory" /> Implements the <see
///     cref="System.IDisposable" />
/// </summary>
/// <seealso cref="ViteCent.Core.Orm.IFactory" />
/// <seealso cref="System.IDisposable" />
public class SqlSugarFactory : IFactory, IDisposable
{
    /// <summary>
    ///     The client
    /// </summary>
    private readonly SqlSugarClient client = default!;

    /// <summary>
    ///     The commands
    /// </summary>
    private readonly List<Command> commands = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlSugarFactory" /> class.
    /// </summary>
    /// <param name="dataBase">The DataBase.</param>
    /// <param name="log">if set to <c>true</c> [log].</param>
    public SqlSugarFactory(string dataBase, bool log = true)
    {
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
                foreach (var p in parameter)
                    if (p.DbType == System.Data.DbType.Int16 || p.DbType == System.Data.DbType.Int32
                                                             || p.DbType == System.Data.DbType.Int64 ||
                                                             p.DbType == System.Data.DbType.Decimal
                                                             || p.DbType == System.Data.DbType.Double ||
                                                             p.DbType == System.Data.DbType.Single
                                                             || p.DbType == System.Data.DbType.UInt16 ||
                                                             p.DbType == System.Data.DbType.UInt32
                                                             || p.DbType == System.Data.DbType.UInt64 ||
                                                             p.DbType == System.Data.DbType.VarNumeric)
                        text = text.Replace(p.ParameterName, p.Value == null ? "" : p.Value.ToString());
                    else
                        text = text.Replace(p.ParameterName, $"'{p.Value ?? default!}'");

                var sql = $"Time: {client.Ado.SqlExecutionTime.TotalMilliseconds} ms, SQL:{text}";
            };
    }

    /// <summary>
    ///     Commits the asynchronous.
    /// </summary>
    /// <returns>A Task&lt;BaseResult&gt; representing the asynchronous operation.</returns>
    public async Task<BaseResult> CommitAsync()
    {
        if (commands.Count > 0)
        {
            client.BeginTran();
            try
            {
                await Task.Run(() =>
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
                });
            }
            catch (Exception e)
            {
                client.RollbackTran();
                return new BaseResult(500, e.Message);
            }
        }

        return new BaseResult();
    }

    /// <summary>
    ///     Deletes the specified where.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="where">The where.</param>
    public void Delete<T>(Expression<Func<T, bool>> where) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Delete, DataType = DataEnum.Where, Where = where });
    }

    /// <summary>
    ///     Deletes the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    public void Delete<T>(T entity) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Delete, DataType = DataEnum.Entity, Entity = entity });
    }

    /// <summary>
    ///     Deletes the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    public void Delete<T>(List<T> entitys) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Delete, DataType = DataEnum.Entity, Entity = entitys });
    }

    /// <summary>
    ///     Deletes the specified SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql">The SQL.</param>
    /// <param name="parameters">The parameters.</param>
    public void Delete<T>(string sql, object parameters = default!) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Delete, DataType = DataEnum.SQL, SQL = sql, Parameters = parameters });
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        client?.Dispose();
    }

    /// <summary>
    ///     Fastests this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>IFastest&lt;T&gt;.</returns>
    public IFastest<T> Fastest<T>() where T : class, new()
    {
        return client.Fastest<T>();
    }

    /// <summary>
    ///     GetFields
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
                .ForMember(x => x.Primarykey, y => y.MapFrom(z => z.IsPrimarykey))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.DbColumnName))
                .ForMember(x => x.Nullable, y => y.MapFrom(z => z.IsNullable))
                .ForMember(x => x.Type, y => y.MapFrom(z => z.DataType));
        });

        var result = new Mapper(config).Map<List<BaseField>>(fields);

        return await Task.FromResult(result);
    }

    /// <summary>
    ///     GetTables
    /// </summary>
    /// <param name="cache"></param>
    /// <returns></returns>
    public async Task<List<BaseTable>> GetTables(bool cache = false)
    {
        var tables = client.DbMaintenance.GetTableInfoList(cache);

        var config = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<DbTableInfo, BaseTable>();
        });

        var result = new Mapper(config).Map<List<BaseTable>>(tables);

        return await Task.FromResult(result);
    }

    /// <summary>
    ///     Inserts the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    public void Insert<T>(T entity) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Insert, DataType = DataEnum.Entity, Entity = entity });
    }

    /// <summary>
    ///     Inserts the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    public void Insert<T>(List<T> entitys) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Insert, DataType = DataEnum.Entity, Entity = entitys });
    }

    /// <summary>
    ///     Inserts the specified SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql">The SQL.</param>
    /// <param name="parameters">The parameters.</param>
    public void Insert<T>(string sql, object parameters = default!) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Insert, DataType = DataEnum.SQL, SQL = sql, Parameters = parameters });
    }

    /// <summary>
    ///     Pages the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
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
    ///     Queries this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>ISugarQueryable&lt;T&gt;.</returns>
    public ISugarQueryable<T> Query<T>() where T : class, new()
    {
        return client.Queryable<T>();
    }

    /// <summary>
    ///     Updates the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    public void Update<T>(T entity) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Update, DataType = DataEnum.Entity, Entity = entity });
    }

    /// <summary>
    ///     Updates the specified Entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The Entity.</param>
    /// <param name="columns">The columns.</param>
    public void Update<T>(T entity, Expression<Func<T, object>> columns) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Update, DataType = DataEnum.Where, Entity = entity, Where = columns });
    }

    /// <summary>
    ///     Updates the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    public void Update<T>(List<T> entitys) where T : class, new()
    {
        commands.Add(new Command { CommandType = CommandEnum.Update, DataType = DataEnum.Entity, Entity = entitys });
    }

    /// <summary>
    ///     Updates the specified Entitys.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entitys">The Entitys.</param>
    /// <param name="columns">The columns.</param>
    public void Update<T>(List<T> entitys, Expression<Func<T, object>> columns) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Update, DataType = DataEnum.Where, Entity = entitys, Where = columns });
    }

    /// <summary>
    ///     Updates the specified SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql">The SQL.</param>
    /// <param name="parameters">The parameters.</param>
    public void Update<T>(string sql, object parameters = default!) where T : class, new()
    {
        commands.Add(new Command
        { CommandType = CommandEnum.Update, DataType = DataEnum.SQL, SQL = sql, Parameters = parameters });
    }

    /// <summary>
    ///     Gets the type of the DataBase.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>DbType.</returns>
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
            _ => DbType.MySql
        };
    }
}