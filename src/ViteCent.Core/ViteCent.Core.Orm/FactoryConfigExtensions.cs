#region

using Microsoft.Extensions.Configuration;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// 数据库配置管理工具类，用于管理和访问数据库连接配置信息
/// </summary>
public static class FactoryConfigExtensions
{
    /// <summary>
    /// 数据库配置集合，存储所有已注册的数据库配置信息
    /// </summary>
    private static readonly List<FactoryConfig> configs = [];

    /// <summary>
    /// 用于线程同步的锁对象，确保配置操作的线程安全
    /// </summary>
    private static readonly object key = new();

    /// <summary>
    /// 根据配置名称获取数据库配置信息
    /// </summary>
    /// <param name="key">配置名称</param>
    /// <returns>返回匹配的数据库配置信息，如果未找到则抛出异常</returns>
    /// <exception cref="Exception">当参数key为空或配置不存在时抛出异常</exception>
    public static FactoryConfig GetConfig(this string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new Exception("参数key不能为空");

        var configuration = configs.FirstOrDefault(x => x.Name == key);

        return configuration ?? throw new Exception("数据库不存在");
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    public static string GetConfiguration(this BaseDatabaseInfo database)
    {
        return database.Type switch
        {
            "MySql" => GetMySqlConfiguration(database),
            "SqlServer" => GetSqlServerConfiguration(database),
            "Sqlite" => GetSqliteConfiguration(database),
            "Oracle" => GetOracleConfiguration(database),
            "PostgreSQL" => GetPostgreSQLConfiguration(database),
            "Dm" => GetDmConfiguration(database),
            "Kdbndp" => GetKdbndpConfiguration(database),
            "Oscar" => GetOscarConfiguration(database),
            "MySqlConnector" => GetMySqlConnectorConfiguration(database),
            "Access" => GetAccessConfiguration(database),
            "OpenGauss" => GetOpenGaussConfiguration(database),
            "QuestDB" => GetQuestDBConfiguration(database),
            "HG" => GetHGConfiguration(database),
            "ClickHouse" => GetClickHouseConfiguration(database),
            "GBase" => GetGBaseConfiguration(database),
            "Odbc" => GetOdbcConfiguration(database),
            "OceanBaseForOracle" => GetOceanBaseForOracleConfiguration(database),
            "TDengine" => GetTDengineConfiguration(database),
            "GaussDB" => GetGaussDBConfiguration(database),
            "OceanBase" => GetOceanBaseConfiguration(database),
            "Tidb" => GetTidbConfiguration(database),
            "Vastbase" => GetVastbaseConfiguration(database),
            "PolarDB" => GetPolarDBConfiguration(database),
            "Doris" => GetDorisConfiguration(database),
            "Xugu" => GetXuguConfiguration(database),
            "GoldenDB" => GetGoldenDBConfiguration(database),
            "TDSQLForPGODBC" => GetTDSQLForPGODBCConfiguration(database),
            "TDSQL" => GetTDSQLConfiguration(database),
            "HANA" => GetHANAConfiguration(database),
            _ => GetCustomConfiguration(database)
        };
    }

    /// <summary>
    /// 设置数据库配置信息，从配置文件中读取并初始化数据库连接信息
    /// </summary>
    /// <param name="configuration">配置信息对象，包含数据库连接相关配置</param>
    /// <exception cref="Exception">当配置文件缺少必要的数据库配置信息时抛出异常</exception>
    public static void SetConfig(this IConfiguration configuration)
    {
        var logger = new BaseLogger(typeof(FactoryConfigExtensions));

        var database = configuration.GetSection("Database") ?? throw new Exception("Appsettings Must Be Database");

        var count = database.GetChildren().Count();

        for (var i = 0; i < count; i++)
        {
            var name = configuration[$"Database:{i}:Name"];

            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Database.Name");

            logger.LogInformation($"Database Name {i} ：{name}");

            var type = configuration[$"Database:{i}:Type"];

            if (string.IsNullOrWhiteSpace(type)) throw new Exception("Database.Type");

            logger.LogInformation($"Database Type {i} ：{type}");

            var master = configuration[$"Database:{i}:Master"];

            if (string.IsNullOrWhiteSpace(master)) throw new Exception("Database.Master");

            logger.LogInformation($"Database Master {i} ：{master}");

            if (IsEncrypt(configuration))
                master = configuration.Decrypt(master);

            var factoryConfig = new FactoryConfig
            {
                Name = name,
                DbType = type,
                Master = master,
                Slaves = []
            };

            var slave = configuration.GetSection($"Database:{i}:Slaves");

            if (slave is not null)
            {
                var slaveCount = slave.GetChildren().Count();

                for (var j = 0; j < slaveCount; j++)
                {
                    var value = configuration[$"Database:{i}:Slaves:{j}"];

                    if (string.IsNullOrWhiteSpace(value)) throw new Exception("Database.Slaves");

                    if (IsEncrypt(configuration))
                        value = configuration.Decrypt(value);

                    logger.LogInformation($"Database Slaves {i} ：{value}");

                    factoryConfig.Slaves.Add(value);
                }
            }
            else
            {
                factoryConfig.Slaves.Add(master);
            }

            lock (key)
            {
                var _config = configs.FirstOrDefault(x => x.Name == factoryConfig.Name);

                if (_config is not null)
                {
                    _config.DbType = factoryConfig.DbType;
                    _config.Master = factoryConfig.Master;
                    _config.Slaves = factoryConfig.Slaves;
                }
                else
                {
                    configs.Add(factoryConfig);
                }
            }
        }
    }

    /// <summary>
    /// 解密数据库连接字符串
    /// </summary>
    /// <param name="input">需要解密的数据库连接字符串</param>
    /// <param name="configuration">包含解密配置信息的配置对象</param>
    /// <returns>解密后的数据库连接字符串</returns>
    /// <exception cref="Exception">当解密类型不支持或配置信息不完整时抛出异常</exception>
    private static string Decrypt(this IConfiguration configuration, string input)
    {
        var type = configuration["Encrypt:Type"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(type))
            throw new Exception("Appsettings Must Be Encrypt:Type");

        if (type == "Base64")
        {
            var bytes = input.DecryptBase64();
            return bytes.ByteToString();
        }

        var key = configuration["Encrypt:Key"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(key))
            throw new Exception("Appsettings Must Be Encrypt:Key");

        return type switch
        {
            "AES" => input.DecryptAES(key),
            "DES" => input.DecryptDES(key),
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support")
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetAccessConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetClickHouseConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetCustomConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetDmConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetDorisConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetGaussDBConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetGBaseConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetGoldenDBConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetHANAConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetHGConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetKdbndpConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetMySqlConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};Port={database.Port};DataBase={database.Name};User={database.User};Password={database.Password};CharSet={database.CharSet};Persistsecurityinfo=True;SslMode=none;allowPublicKeyRetrieval=True;";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetMySqlConnectorConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetOceanBaseConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetOceanBaseForOracleConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetOdbcConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetOpenGaussConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetOracleConfiguration(this BaseDatabaseInfo database)
    {
        return $"User Id={database.User};Password={database.Password};Data Source={database.Name};";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetOscarConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetPolarDBConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetPostgreSQLConfiguration(this BaseDatabaseInfo database)
    {
        return $"Host={database.Server};Username=postgres;Password=123456;Database=gisdb;Pooling=false;";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetQuestDBConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetSqliteConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetSqlServerConfiguration(this BaseDatabaseInfo database)
    {
        return $"Data Source={database.Server};Initial Catalog={database.Name};User ID={database.User};Password={database.Password};MultipleActiveResultSets=true";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetTDengineConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetTDSQLConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetTDSQLForPGODBCConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetTidbConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetVastbaseConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// </summary>
    /// <param name="database">数据库信息</param>
    /// <returns>处理结果</returns>
    private static string GetXuguConfiguration(this BaseDatabaseInfo database)
    {
        return $"Server={database.Server};User Id={database.User};Password={database.Password};DATABASE={database.Name}";
    }

    /// <summary>
    /// 判断是否启用数据库连接字符串加密
    /// </summary>
    /// <param name="configuration">配置信息对象</param>
    /// <returns>如果启用加密返回true，否则返回false</returns>
    private static bool IsEncrypt(this IConfiguration configuration)
    {
        var _switch = configuration["Encrypt:Switch"] ?? string.Empty;

        var result = bool.TryParse(_switch, out var flag);

        if (!result || !flag)
            return false;

        return true;
    }
}