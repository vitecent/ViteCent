#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Elasticsearch;

/// <summary>
/// Elasticsearch配置扩展类
/// </summary>
public static class ElasticsearchExtensions
{
    /// <summary>
    /// 注册Elasticsearch服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置信息</param>
    /// <returns>返回服务集合</returns>
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var strConn = configuration["Elasticsearch"];

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Elasticsearch");

        services.AddTransient<IElasticsearch>(x => new ElasticsearchFactory(strConn));

        return services;
    }
}