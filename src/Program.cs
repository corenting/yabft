﻿using System;
using System.IO;

namespace yabft
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: yabft -f file or yabft program_string");
                return;
            }

            string program = "";
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

            var interpreter = new Interpreter.Runner(program);
            interpreter.Run();
        }
    }
}