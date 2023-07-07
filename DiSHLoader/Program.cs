using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using dotenv.net;
using Microsoft.Win32;
using DiSHCore;

static string logMessages(string message)
{
    Console.WriteLine("DiSH : " + message);
    return "";
}

#nullable disable
string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string DefaultVersion = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\LDevs\\DiSHLoader\\", "DefaultVersion", null);
string EnvFilePath = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\LDevs\\DiSHLoader\\", "EnvPath", AppDataPath + "\\LDevs\\DiSHLoader\\.env");
string VersionsPath = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\LDevs\\DiSHLoader\\", "EnvPath", AppDataPath + "\\LDevs\\DiSHLoader\\versions\\");
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
    Console.WriteLine(exitCode);
    switch (exitCode)
    {
        case 0:
            System.Environment.Exit(exitCode);
            break;
        case 2:
            _ = Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location);
            System.Environment.Exit(0);
            break;
        case 1:
            DiSH dish = new DiSH(Environment.GetEnvironmentVariable("TOKEN"),
                (ulong)Decimal.Parse(Environment.GetEnvironmentVariable("GUILD_ID")), 
                (ulong)Decimal.Parse(Environment.GetEnvironmentVariable("CATEGORY_ID")), 
                (ulong)Decimal.Parse(Environment.GetEnvironmentVariable("LOGS_ID")), 
                logMessages);
            Task Run() => dish.RunBot();
            Run().Wait();
            break;
    }
}
