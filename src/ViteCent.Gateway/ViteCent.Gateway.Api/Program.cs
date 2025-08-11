/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Gateway.Api;

/// <summary>
/// 网关服务程序入口
/// </summary>
public class Program
{
    /// <summary>
    /// 主方法
    /// </summary>
    /// <param name="args">启动参数</param>
    public static async Task Main(string[] args)
    {
        // 配置XML文档路径，用于Swagger接口文档生成
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Gateway.*.xml"
        };

        // 创建并配置微服务实例
        var microService = new BaseMicroService("ViteCent.Gateway.Service", xmls)
        {
            // 配置服务构建
            OnBuild = builder =>
            {
                // 配置AutoMapper对象映射
                builder.UseAutoMapper(typeof(AutoMapperConfig));
                // 配置AutoFac依赖注入容器
                builder.UseAutoFac(new AutoFacConfig());
                // 添加请求解密中间件
                builder.Services.AddDecryptRequest();
                // 添加网关服务
                builder.Services.AddGateway();
                // 添加响应加密中间件
                builder.Services.AddEncryptResponse();
            },
            // 配置应用启动
            OnStart = app =>
            {
                // 使用请求解密中间件
                app.UseDecryptRequest();
                // 使用网关中间件
                app.UseGateway();
                // 使用响应加密中间件
                app.UseEncryptResponse();
            }
        };

        await microService.RunAsync(args);
    }
}