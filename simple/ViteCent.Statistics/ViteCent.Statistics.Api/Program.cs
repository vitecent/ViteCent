#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Statistics.Api;

/// <summary>
/// 程序入口
/// </summary>
public class Program
{
    /// <summary>
    /// 主方法
    /// </summary>
    /// <param name="args"></param>
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