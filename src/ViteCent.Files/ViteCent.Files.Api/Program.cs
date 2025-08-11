/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Files.Api;

/// <summary>
/// 程序入口
/// </summary>
public class Program
{
    /// <summary>
    /// 主方法
    /// </summary>
    /// <param name="args">启动参数</param>
    public static async Task Main(string[] args)
    {
        // 配置API文档XML路径
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",    // 核心库API文档
            "ViteCent.Files.*.xml"    // 文件服务API文档
        };

        // 创建文件服务微服务实例
        var microService = new FilesMicroService("ViteCent.Files.Service", xmls);

        // 启动微服务
        await microService.RunAsync(args);
    }
}