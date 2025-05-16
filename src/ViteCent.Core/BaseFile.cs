#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// 提供文件操作的工具类，包括文件的复制、创建、剪切、删除、读取和写入等基本操作
/// </summary>
public class BaseFile
{
    /// <summary>
    /// 复制文件到指定位置
    /// </summary>
    /// <param name="from">源文件路径</param>
    /// <param name="to">目标文件路径</param>
    /// <returns>复制成功返回true，否则返回false</returns>
    public static bool Copy(string from, string to)
    {
        if (File.Exists(from) && from != to)
        {
            File.Copy(from, to, true);

            return true;
        }

        return false;
    }

    /// <summary>
    /// 创建文件，如果文件所在目录不存在则自动创建目录
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>创建成功返回true</returns>
    public static bool Create(string path)
    {
        var _path = path.Replace(@"\", @"/");
        var __path = Path.GetDirectoryName(path) ?? default!;

        if (!Directory.Exists(__path)) Directory.CreateDirectory(__path);

        if (!File.Exists(_path)) File.Create(_path).Close();

        return true;
    }

    /// <summary>
    /// 剪切文件到指定位置（复制后删除源文件）
    /// </summary>
    /// <param name="from">源文件路径</param>
    /// <param name="to">目标文件路径</param>
    /// <returns>剪切成功返回true，否则返回false</returns>
    public static bool Cut(string from, string to)
    {
        if (Copy(from, to)) return Delete(from);

        return false;
    }

    /// <summary>
    /// 删除指定文件，默认执行3次安全删除
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>删除成功返回true，否则返回false</returns>
    public static bool Delete(string path)
    {
        return Delete(path, 3);
    }

    /// <summary>
    /// 安全删除文件，可指定删除次数
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="times">删除次数，每次删除都会用随机数据覆盖文件内容</param>
    /// <returns>删除成功返回true，否则返回false</returns>
    public static bool Delete(string path, int times)
    {
        DeleteMethod(path, times);

        return !File.Exists(path);
    }

    /// <summary>
    /// 获取文件的字节数组
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件的字节数组</returns>
    public static byte[] GetFileByte(string path)
    {
        return GetFileStream(path).StreamToByte();
    }

    /// <summary>
    /// 获取文件的流对象
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件的Stream对象</returns>
    public static Stream GetFileStream(string path)
    {
        return Open(path).StringToStream();
    }

    /// <summary>
    /// 以UTF8编码打开并读取文件内容
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件内容字符串，如果文件不存在则返回默认值</returns>
    public static string Open(string path)
    {
        return Open(path, Encoding.UTF8);
    }

    /// <summary>
    /// 使用指定编码打开并读取文件内容
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="encoding">文件编码</param>
    /// <returns>文件内容字符串，如果文件不存在则返回默认值</returns>
    public static string Open(string path, Encoding encoding)
    {
        if (File.Exists(path))
        {
            var sr = new StreamReader(path, encoding);

            return sr.ReadToEnd();
        }

        return default!;
    }

    /// <summary>
    /// 将字符串内容写入文件，如果文件不存在则创建
    /// </summary>
    /// <param name="input">要写入的字符串内容</param>
    /// <param name="path">文件路径</param>
    /// <returns>写入成功返回true</returns>
    public static bool Write(string input, string path)
    {
        Create(path);
        using var sw = new StreamWriter(path);
        sw.Write(input);

        return true;
    }

    /// <summary>
    /// 执行文件安全删除的内部方法，通过多次覆盖文件内容来确保数据不可恢复
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="times">删除次数</param>
    private static void DeleteMethod(string path, int times)
    {
        if (!File.Exists(path)) throw new FileNotFoundException(path);

        File.SetAttributes(path, FileAttributes.Normal);
        var number = (int)Math.Ceiling(new FileInfo(path).Length / 512.0);
        var bytes = new byte[512];
        var rng = RandomNumberGenerator.Create();

        using (var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            for (var i = 0; i < times; i++)
            {
                fs.Position = 0;
                for (var j = 0; j < number; j++)
                {
                    rng.GetBytes(bytes);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            fs.SetLength(0);
            fs.Close();
        }

        var time = DateTime.Now.AddYears(100);
        File.SetCreationTime(path, time);
        File.SetLastAccessTime(path, time);
        File.SetLastWriteTime(path, time);
        File.Delete(path);
    }
}