/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Auth.Api;

/// <summary>
/// ViteCent.Auth服务API程序入口类
/// </summary>
/// <remarks>
/// 该类负责配置和启动ViteCent.Auth服务的Web API应用程序，主要功能包括：
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
    /// 1. 配置XML文档路径，包括和ViteCent.Auth的XML文档
    /// 2. 创建并配置微服务实例
    /// 3. 注册AutoMapper配置，用于对象映射
    /// 4. 注册AutoFac配置，实现依赖注入
    /// 5. 异步启动微服务
    /// </remarks>
    public static async Task Main(string[] args)
    {
        // 配置XML文档路径，用于Swagger接口文档生成
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Auth.*.xml"
        };

        // 创建并配置微服务实例
        var microService = new BaseMicroService("ViteCent.Auth.Api", xmls)
        {
            // 配置服务构建
            OnBuild = builder =>
            {
                // 配置AutoMapper对象映射
                builder.UseAutoMapper(typeof(AutoMapperConfig));
                // 配置AutoFac依赖注入容器
                builder.UseAutoFac(new AutoFacConfig());
            }
        };

        await microService.RunAsync(args);
    }
}