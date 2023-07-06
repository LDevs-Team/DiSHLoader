using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using dotenv.net;
using Microsoft.Win32;



string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string DefaultVersion = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\LDevs\\DiSHLoader\\", "DefaultVersion", null).ToString();
string EnvFilePath = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\LDevs\\DiSHLoader\\", "EnvPath", AppDataPath + "\\LDevs\\DiSHLoader\\.env").ToString();
string VersionsPath = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\LDevs\\DiSHLoader\\", "EnvPath", AppDataPath + "\\LDevs\\DiSHLoader\\versions\\").ToString();
Console.WriteLine("Default version -> " + DefaultVersion);
Console.WriteLine("Env file -> " + EnvFilePath);
Console.WriteLine("Default version path -> " + VersionsPath + DefaultVersion + "\\");
DotEnv.Load(new DotEnvOptions(true, new string[] { EnvFilePath }));
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
