using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class NVelocityExpand
{
    /// <summary>
    /// </summary>
    private readonly VelocityContext vc = default!;

    /// <summary>
    /// </summary>
    private readonly VelocityEngine ve = default!;

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    public NVelocityExpand(string path)
    {
        ve = new VelocityEngine();
        var eps = new ExtendedProperties();
        eps.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
        eps.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");
        eps.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
        eps.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, path);
        ve.Init(eps);

        vc = new VelocityContext();
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string Display(string path)
    {
        var vm = ve.GetTemplate(path);
        using var sw = new StringWriter();
        vm.Merge(vc, sw);
        return sw.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Put(string key, object value)
    {
        vc.Put(key, value);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
    public bool Save(string path, string savePath)
    {
        var vm = ve.GetTemplate(path);
        using var sw = new StringWriter();
        vm.Merge(vc, sw);
        var input = sw.ToString();
        input = input.Replace("\r\n\r\n}", "\r\n}");
        var flag = BaseFile.Write(input, savePath);
        sw.Close();
        return flag;
    }
}