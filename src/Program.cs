namespace Yabft
{
    using System;
    using System.IO;
    using Yabft.InputOuput;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: yabft -f file or yabft program_string");
                return;
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
                        Console.WriteLine("File loading error");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Please specify file path");
                    return;
                }
            }
            else
            {
                program = args[0];
            }

            var interpreter = new Interpreter.Runner(new ConsoleInputOutput(), program);
            interpreter.Run();
        }
    }
}
