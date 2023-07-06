using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using dotenv.net;
using Microsoft.Win32;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal class Program
{
    // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
    private static void Main(string[] args)
    {
        string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string DefaultVersion = Registry.GetValue("HKEY_CURRENT_USER\\LDevs\\DiSHLoader\\", "DefaultVersion", null).ToString();
        object value = Registry.GetValue("HKEY_CURRENT_USER\\LDevs\\DiSHLoader\\", "EnvPath", AppDataPath + "\\DiSHLoader\\.env");
        string EnvFilePath = ((value != null) ? value.ToString() : null);
        string VersionsPath = Registry.GetValue("HKEY_CURRENT_USER\\LDevs\\DiSHLoader\\", "EnvPath", AppDataPath + "\\DiSHLoader\\versions\\").ToString();
        Console.WriteLine("Default version -> " + DefaultVersion);
        Console.WriteLine("Env file -> " + EnvFilePath);
        Console.WriteLine("Default version path -> " + VersionsPath + DefaultVersion + "\\");
        DotEnv.Load(new DotEnvOptions(true, new string[] { EnvFilePath } ));
        Console.WriteLine(Environment.GetEnvironmentVariable("GUILD_ID"));
        var procInfo = new ProcessStartInfo();
        procInfo.FileName = VersionsPath + DefaultVersion + "\\DiSH.exe";
        procInfo.WindowStyle = ProcessWindowStyle.Hidden;
        procInfo.CreateNoWindow = true;
        using (Process proc = Process.Start(procInfo))
        {
            if (proc != null)
            {
                proc.WaitForExit();
            }
            int exitCode = proc.ExitCode;
        }
    }
}