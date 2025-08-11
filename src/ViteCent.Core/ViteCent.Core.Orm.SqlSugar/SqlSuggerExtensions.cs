#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// SqlSugar ORM配置扩展类
/// </summary>
public static class SqlSuggerExtensions
{
    /// <summary>
    /// 添加SqlSugar ORM服务
    /// </summary>
    /// <param name="services">IServiceCollection服务集合</param>
    /// <param name="configuration">IConfiguration配置接口</param>
    /// <returns>返回服务集合</returns>
    public static IServiceCollection AddSqlSugger(this IServiceCollection services, IConfiguration configuration)
    {
        FactoryConfigExtensions.SetConfig(configuration);

        return services;
    }
}