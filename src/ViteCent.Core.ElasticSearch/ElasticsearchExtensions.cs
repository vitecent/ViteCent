#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Elasticsearch;

/// <summary>
/// </summary>
public static class ElasticsearchExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var strConn = configuration["Elasticsearch"];

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Elasticsearch");

        services.AddTransient<IElasticsearch>(x => new ElasticsearchFactory(strConn));

        return services;
    }
}