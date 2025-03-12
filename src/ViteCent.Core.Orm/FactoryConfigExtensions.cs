#region

using Microsoft.Extensions.Configuration;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class FactoryConfigExtensions
{
    /// <summary>
    /// </summary>
    private static readonly List<FactoryConfig> configs = [];

    /// <summary>
    /// </summary>
    private static readonly object key = new();

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static FactoryConfig GetConfig(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new Exception("参数key不能为空");

        var configuration = configs.FirstOrDefault(x => x.Name == key);

        return configuration ?? throw new Exception("数据库不存在");
    }

    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    public static void SetConfig(IConfiguration configuration)
    {
        var logger = BaseLogger.GetLogger();

        var dataBase = configuration.GetSection("DataBase") ?? throw new Exception("Appsettings Must Be DataBase");

        var dataBaseCount = dataBase.GetChildren().Count();

        for (var i = 0; i < dataBaseCount; i++)
        {
            var name = configuration[$"DataBase:{i}:Name"];

            if (string.IsNullOrWhiteSpace(name)) throw new Exception("DataBase.Name");

            logger.Info($"DataBase Name {i} ：{name}");

            var type = configuration[$"DataBase:{i}:Type"];

            if (string.IsNullOrWhiteSpace(type)) throw new Exception("DataBase.Type");

            logger.Info($"DataBase Type {i} ：{type}");

            var master = configuration[$"DataBase:{i}:Master"];

            if (string.IsNullOrWhiteSpace(master)) throw new Exception("DataBase.Master");

            logger.Info($"DataBase Master {i} ：{master}");

            if (IsEncrypt(configuration))
                master = Encrypt(master, configuration);

            var factoryConfig = new FactoryConfig
            {
                Name = name,
                DbType = type,
                Master = master,
                Slaves = []
            };

            var slave = configuration.GetSection($"DataBase:{i}:Slaves");

            if (slave != null)
            {
                var slaveCount = slave.GetChildren().Count();

                for (var j = 0; j < slaveCount; j++)
                {
                    var value = configuration[$"DataBase:{i}:Slaves:{j}"];

                    if (string.IsNullOrWhiteSpace(value)) throw new Exception("DataBase.Slaves");

                    if (IsEncrypt(configuration))
                        value = Encrypt(value, configuration);

                    logger.Info($"DataBase Slaves {i} ：{value}");

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

                if (_config != null)
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
    /// </summary>
    /// <param name="input"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static string Encrypt(string input, IConfiguration configuration)
    {
        var type = configuration["Encrypt:Type"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(type))
            throw new Exception("Appsettings Must Be Encrypt:Type");

        if (type == "Base64")
        {
            var bytes = input.StringToByte();
            return bytes.EncryptBase64();
        }

        var key = configuration["Encrypt:Key"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(key))
            throw new Exception("Appsettings Must Be Encrypt:Key");

        return type switch
        {
            "AES" => input.EncryptAES(key),
            "DES" => input.EncryptDES(key),
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support"),
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static bool IsEncrypt(IConfiguration configuration)
    {
        var _switch = configuration["Encrypt:Switch"] ?? string.Empty;

        var result = bool.TryParse(_switch, out var flag);

        if (!result || !flag)
            return false;

        return true;
    }
}