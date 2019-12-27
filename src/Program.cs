namespace Yabft
{
    using System;
    using System.IO;
    using Yabft.InputOuput;
    using Yabft.Shared;

    public class Program
    {
        /// <summary>
        /// Yet Another Brainfuck Thing.
        /// </summary>
        /// <param name="file">Brainfuck program file to load.</param>
        /// <param name="argument">Brainfuck program as a string.</param>
        /// <param name="wrap">Enable wrapping of bytes in cell.</param>
        public static int Main(FileInfo file = null, string argument = null, bool wrap = false)
        {
            string brainfuckProgram;
            if (file != null)
            {
                try
                {
                    brainfuckProgram = File.ReadAllText(Path.GetFullPath(file.FullName));
                }
                catch
                {
                    Console.Error.WriteLine("File loading error");
                    return 1;
                }
            }
            else
            {
                brainfuckProgram = argument;
            }

            // Check program before running it
            bool isValid = Parser.IsValid(brainfuckProgram);
            if (!isValid)
            {
                Console.Error.WriteLine("Invalid program input");
                return 1;
            }

            // Parse runner options
            RunnerOptions runnerOptions = new RunnerOptions
            {
                Wrap = wrap,
                InputOutputSystem = new ConsoleInputOutput(),
            };

            var interpreter = new Runner.Interpreter(runnerOptions, brainfuckProgram);
            interpreter.Run();

            return 0;
        }
    }
}
