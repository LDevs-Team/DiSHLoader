using Microsoft.Win32;

string keyPath = @"HKEY_CURRENT_USER\Software\LDevs\DiSHLoader\";
string valueName = "LatestVersion";



Registry.SetValue(keyPath, valueName, "10.2");


object testValue = Registry.GetValue(keyPath, valueName, null);
Console.WriteLine(testValue);
