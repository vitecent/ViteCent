using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace ViteCent.Core;

/// <summary>
/// NVelocity模板引擎扩展类，提供模板渲染、变量设置和保存等功能
/// </summary>
public class NVelocityExpand
{
    /// <summary>
    /// Velocity上下文对象，用于存储模板变量
    /// </summary>
    private readonly VelocityContext vc = default!;

    /// <summary>
    /// Velocity引擎实例，用于模板解析和渲染
    /// </summary>
    private readonly VelocityEngine ve = default!;

    /// <summary>
    /// 初始化NVelocity模板引擎扩展类
    /// </summary>
    /// <param name="path">模板文件所在的目录路径</param>
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
    /// 渲染指定路径的模板并返回渲染结果
    /// </summary>
    /// <param name="path">模板文件的相对路径</param>
    /// <returns>渲染后的字符串内容</returns>
    public string Display(string path)
    {
        var vm = ve.GetTemplate(path);
        using var sw = new StringWriter();
        vm.Merge(vc, sw);
        return sw.ToString();
    }

    /// <summary>
    /// 向模板上下文中添加变量
    /// </summary>
    /// <param name="key">变量名称</param>
    /// <param name="value">变量值</param>
    public void Put(string key, object value)
    {
        vc.Put(key, value);
    }

    /// <summary>
    /// 渲染指定路径的模板并保存到目标文件
    /// </summary>
    /// <param name="path">模板文件的相对路径</param>
    /// <param name="savePath">保存目标文件的完整路径</param>
    /// <returns>保存是否成功</returns>
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