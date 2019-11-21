namespace Tests
{
    using System;
    using System.IO;
    using System.Text;

    public static class Utils
    {
        public static string LoadBrainFuckProgram(string programName)
        {
            string solutionPath = Directory
                .GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent.Parent.Parent.Parent.FullName;
            string programPath = Path.Combine(solutionPath, "brainfuck_programs", programName + ".b");
            return File.ReadAllText(programPath, Encoding.UTF8);
        }
    }
}
