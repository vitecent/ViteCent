#region

using System.Diagnostics;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class ProcessExtensions.
/// </summary>
public class BaseProcess
{
    /// <summary>
    ///     Executes the specified command.
    /// </summary>
    /// <param name="cmd">The command.</param>
    /// <returns>System.String.</returns>
    public static string Execute(string cmd)
    {
        return Execute(cmd, default!);
    }

    /// <summary>
    ///     Executes the specified command.
    /// </summary>
    /// <param name="cmd">The command.</param>
    /// <param name="arguments"></param>
    /// <returns>System.String.</returns>
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