#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Gateway.Api;

/// <summary>
/// </summary>
public class Program
{
    /// <summary>
    /// </summary>
    public static async Task Main(string[] args)
    {
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Gateway.*.xml"
        };

        var microService = new BaseMicroService("ViteCent.Gateway.Service", xmls)
        {
            OnBuild = builder =>
            {
                builder.UseAutoMapper(typeof(AutoMapperConfig));
                builder.UseAutoFac(new AutoFacConfig());
                builder.Services.AddDecryptRequest();
                builder.Services.AddGateway();
                builder.Services.AddEncryptResponse();
            },
            OnStart = app =>
            {
                app.UseDecryptRequest();
                app.UseGateway();
                app.UseEncryptResponse();
            }
        };

        await microService.RunAsync(args);
    }
}