#region

using Microsoft.Extensions.Configuration;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// 数据库配置管理工具类，用于管理和访问数据库连接配置信息
/// </summary>
public class FactoryConfigExtensions
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
    public static FactoryConfig GetConfig(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new Exception("参数key不能为空");

        var configuration = configs.FirstOrDefault(x => x.Name == key);

        return configuration ?? throw new Exception("数据库不存在");
    }

    /// <summary>
    /// 设置数据库配置信息，从配置文件中读取并初始化数据库连接信息
    /// </summary>
    /// <param name="configuration">配置信息对象，包含数据库连接相关配置</param>
    /// <exception cref="Exception">当配置文件缺少必要的数据库配置信息时抛出异常</exception>
    public static void SetConfig(IConfiguration configuration)
    {
        var logger = new BaseLogger(typeof(FactoryConfigExtensions));

        var dataBase = configuration.GetSection("DataBase") ?? throw new Exception("Appsettings Must Be DataBase");

        var dataBaseCount = dataBase.GetChildren().Count();

        for (var i = 0; i < dataBaseCount; i++)
        {
            var name = configuration[$"DataBase:{i}:Name"];

            if (string.IsNullOrWhiteSpace(name)) throw new Exception("DataBase.Name");

            logger.LogInformation($"DataBase Name {i} ：{name}");

            var type = configuration[$"DataBase:{i}:Type"];

            if (string.IsNullOrWhiteSpace(type)) throw new Exception("DataBase.Type");

            logger.LogInformation($"DataBase Type {i} ：{type}");

            var master = configuration[$"DataBase:{i}:Master"];

            if (string.IsNullOrWhiteSpace(master)) throw new Exception("DataBase.Master");

            logger.LogInformation($"DataBase Master {i} ：{master}");

            if (IsEncrypt(configuration))
                master = Decrypt(master, configuration);

            var factoryConfig = new FactoryConfig
            {
                Name = name,
                DbType = type,
                Master = master,
                Slaves = []
            };

            var slave = configuration.GetSection($"DataBase:{i}:Slaves");

            if (slave is not null)
            {
                var slaveCount = slave.GetChildren().Count();

                for (var j = 0; j < slaveCount; j++)
                {
                    var value = configuration[$"DataBase:{i}:Slaves:{j}"];

                    if (string.IsNullOrWhiteSpace(value)) throw new Exception("DataBase.Slaves");

                    if (IsEncrypt(configuration))
                        value = Decrypt(value, configuration);

                    logger.LogInformation($"DataBase Slaves {i} ：{value}");

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
    private static string Decrypt(string input, IConfiguration configuration)
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
    /// 判断是否启用数据库连接字符串加密
    /// </summary>
    /// <param name="configuration">配置信息对象</param>
    /// <returns>如果启用加密返回true，否则返回false</returns>
    private static bool IsEncrypt(IConfiguration configuration)
    {
        var _switch = configuration["Encrypt:Switch"] ?? string.Empty;

        var result = bool.TryParse(_switch, out var flag);

        if (!result || !flag)
            return false;

        return true;
    }
}