namespace Tests;

using System;
using System.IO;
using System.Text;

public static class Utils
{
    public static string LoadBrainFuckProgram(string programName)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var programPath = Path.Combine(basePath, programName + ".b");
        return File.ReadAllText(programPath, Encoding.UTF8);
    }
}
