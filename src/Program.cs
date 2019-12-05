namespace Yabft
{
    using System;
    using System.IO;
    using Yabft.InputOuput;
    using Yabft.Shared;

    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: yabft -f file or yabft program_string");
                return 1;
            }

            string program = string.Empty;
            if (args[0] == "-f")
            {
                if (args.Length == 2)
                {
                    try
                    {
                        program = File.ReadAllText(Path.GetFullPath(args[1]));
                    }
                    catch
                    {
                        Console.Error.WriteLine("File loading error");
                        return 1;
                    }
                }
                else
                {
                    Console.Error.WriteLine("Please specify file path");
                    return 1;
                }
            }
            else
            {
                program = args[0];
            }

            // Check program before running it
            bool isValid = Parser.IsValid(program);
            if (!isValid)
            {
                Console.Error.WriteLine("Invalid program input");
                return 1;
            }

            var interpreter = new Runner.Interpreter(new ConsoleInputOutput(), program);
            interpreter.Run();

            return 0;
        }
    }
}
