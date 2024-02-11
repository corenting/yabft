namespace Yabft;

using System;
using System.IO;
using Yabft.Common;
using Yabft.InputOuput;

public class Program
{
    /// <summary>
    /// Yet Another Brainfuck Thing.
    /// </summary>
    /// <param name="argument">Brainfuck program as a string.</param>
    /// <param name="wrap">Enable wrapping of bytes in cell.</param>
    public static int Main(string argument, bool wrap = false)
    {
        // Check if argument is a path or not
        string brainfuckProgram;
        try
        {
            var fullPath = Path.GetFullPath(argument);
            brainfuckProgram = File.ReadAllText(fullPath);
        }
        catch (IOException)
        {
            brainfuckProgram = argument;
        }

        // Check program before running it
        var isValid = Parser.IsValid(brainfuckProgram);
        if (!isValid)
        {
            Console.Error.WriteLine("Invalid program input");
            return 1;
        }

        // Parse runner options
        var runnerOptions = new RunnerOptions
        (
            wrap,
            new ConsoleInputOutput()
        );

        var interpreter = new Runner.Interpreter(runnerOptions, brainfuckProgram);
        interpreter.Run();

        return 0;
    }
}
