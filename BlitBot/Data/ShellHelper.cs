using System.Diagnostics;

namespace BlitBot.Data;

public static class ShellHelper
{
    public static void LinuxSleepScreen(bool state) 
    {
        var onOrOff = state ? "on" : "off";

        var cmd = $"DISPLAY=:0.0 xset dpms force {onOrOff}";

        cmd.Bash();
    }

    public static string Bash(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return result;
    }
}
