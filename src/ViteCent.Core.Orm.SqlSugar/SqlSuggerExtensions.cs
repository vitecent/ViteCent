#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// </summary>
public static class SqlSuggerExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugger(this IServiceCollection services, IConfiguration configuration)
    {
        FactoryConfigExtensions.SetConfig(configuration);

        return services;
    }
}