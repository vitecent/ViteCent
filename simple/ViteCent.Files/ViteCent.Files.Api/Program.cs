#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Files.Api;

/// <summary>
/// </summary>
public class Program
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    public static async Task Main(string[] args)
    {
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Files.*.xml"
        };

        var microService = new FilesMicroService("ViteCent.Files.Service", xmls);
        await microService.RunAsync(args);
    }
}