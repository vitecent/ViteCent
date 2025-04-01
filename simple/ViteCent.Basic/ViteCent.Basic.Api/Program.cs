#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Api;

/// <summary>
/// �������
/// </summary>
public class Program
{
    /// <summary>
    /// ������
    /// </summary>
    /// <param name="args"></param>
    public static async Task Main(string[] args)
    {
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Basic.*.xml"
        };

        var microService = new BaseMicroService("ViteCent.Basic.Api", xmls)
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