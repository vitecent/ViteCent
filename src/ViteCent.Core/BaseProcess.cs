#region

using System.Diagnostics;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class BaseProcess
{
    /// <summary>
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public static string Execute(string cmd)
    {
        return Execute(cmd, default!);
    }

    /// <summary>
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
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