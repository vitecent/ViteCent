#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
///     Class SqlSuggerExtensions.
/// </summary>
public static class SqlSuggerExtensions
{
    /// <summary>
    ///     Adds the SQL sugger.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddSqlSugger(this IServiceCollection services, IConfiguration configuration)
    {
        FactoryConfigExtensions.SetConfig(configuration);

        return services;
    }
}