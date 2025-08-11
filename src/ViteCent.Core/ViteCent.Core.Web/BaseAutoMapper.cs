#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// AutoMapper对象映射配置工具类
/// </summary>
public static class BaseAutoMapper
{
    /// <summary>
    /// 配置AutoMapper对象映射
    /// </summary>
    /// <param name="builder">Web应用构建器</param>
    /// <param name="types">需要进行对象映射的类型数组</param>
    public static void UseAutoMapper(this WebApplicationBuilder builder, params Type[] types)
    {
        builder.Services.AddAutoMapper((config) =>
        {
            config.AddMaps(types);
        });
    }
}