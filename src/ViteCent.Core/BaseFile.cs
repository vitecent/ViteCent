#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class BaseFile
{
    /// <summary>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Create(string path)
    {
        var _path = path.Replace(@"\", @"/");
        var __path = Path.GetDirectoryName(path) ?? default!;

        if (!Directory.Exists(__path)) Directory.CreateDirectory(__path);

        if (!File.Exists(_path)) File.Create(_path).Close();

        return true;
    }

    /// <summary>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static bool Cut(string from, string to)
    {
        if (Copy(from, to)) return Delete(from);

        return false;
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Delete(string path)
    {
        return Delete(path, 3);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public static bool Delete(string path, int times)
    {
        DeleteMethod(path, times);

        return !File.Exists(path);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] GetFileByte(string path)
    {
        return GetFileStream(path).StreamToByte();
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Stream GetFileStream(string path)
    {
        return Open(path).StringToStream();
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Open(string path)
    {
        return Open(path, Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="input"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Write(string input, string path)
    {
        Create(path);
        using var sw = new StreamWriter(path);
        sw.Write(input);

        return true;
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="times"></param>
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