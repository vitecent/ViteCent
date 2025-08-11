#region

using System.Diagnostics;

#endregion

namespace ViteCent.Core;

/// <summary>
/// 进程执行工具类，提供命令行进程的创建和执行功能
/// </summary>
public class BaseProcess
{
    /// <summary>
    /// 执行指定的命令行程序
    /// </summary>
    /// <param name="cmd">要执行的命令行程序路径</param>
    /// <returns>命令执行的输出结果字符串。如果执行超时，则返回默认值</returns>
    public static string Execute(string cmd)
    {
        return Execute(cmd, string.Empty);
    }

    /// <summary>
    /// 执行指定的命令行程序，并传入命令行参数
    /// </summary>
    /// <param name="cmd">要执行的命令行程序路径</param>
    /// <param name="arguments">命令行参数</param>
    /// <returns>命令执行的输出结果字符串。如果执行超时，则返回默认值</returns>
    public static string Execute(string cmd, string arguments)
    {
        var p = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = cmd,
            CreateNoWindow = true,
            RedirectStandardInput = true,
            RedirectStandardError = false,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            ErrorDialog = false,
            WorkingDirectory = Environment.CurrentDirectory,
            Verb = "runas"
        };

        if (!string.IsNullOrWhiteSpace(arguments)) startInfo.Arguments = arguments;

        p.StartInfo = startInfo;

        p.Start();
        var i = 1;

        while (!p.HasExited)
        {
            i++;
            p.WaitForExit(500);
            if (i == 5)
            {
                p.Kill();
                return default!;
            }
        }

        var result = p.StandardOutput.ReadToEnd();
        p.Close();

        return result;
    }
}