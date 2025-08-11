#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Statistics.Api;

/// <summary>
/// 统计服务API程序入口类
/// </summary>
/// <remarks>
/// 该类负责配置和启动统计服务的Web API应用程序，包括：
/// 1. 配置XML文档路径，用于Swagger接口文档生成
/// 2. 初始化微服务配置，包括AutoMapper和AutoFac的配置
/// 3. 启动微服务应用程序
/// </remarks>
public class Program
{
    /// <summary>
    /// 应用程序入口主方法
    /// </summary>
    /// <param name="args">启动参数数组</param>
    /// <remarks>
    /// 主要完成以下初始化工作：
    /// 1. 配置XML文档路径，包括Core层和Statistics层的XML文档
    /// 2. 创建并配置微服务实例
    /// 3. 注册AutoMapper配置，用于对象映射
    /// 4. 注册AutoFac配置，实现依赖注入
    /// 5. 启动微服务
    /// </remarks>
    public static async Task Main(string[] args)
    {
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Statistics.*.xml"
        };

        var microService = new BaseMicroService("ViteCent.Statistics.Api", xmls)
        {
            OnBuild = builder =>
            {
                builder.UseAutoMapper(typeof(AutoMapperConfig));
                builder.UseAutoFac(new AutoFacConfig());
            }
        };

        await microService.RunAsync(args);
    }
}