namespace Tests
{
    using System;
    using System.IO;
    using System.Text;

    public static class Utils
    {
        public static string LoadBrainFuckProgram(string programName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string programPath = Path.Combine(basePath, programName + ".b");
            return File.ReadAllText(programPath, Encoding.UTF8);
        }
    }
}
