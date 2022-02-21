namespace Tests.InputOutput;

using System;
using System.IO;
using Xunit;
using Yabft.InputOuput;

public class ConsoleInputOutputTests : IDisposable
{
    private readonly ConsoleInputOutput consoleInputOutput;

    private readonly StringWriter mockedOutputWriter;
    private readonly TextWriter originalOutputWriter;

    public ConsoleInputOutputTests()
    {
        this.originalOutputWriter = Console.Out;
        this.mockedOutputWriter = new StringWriter();
        Console.SetOut(this.mockedOutputWriter);

        this.consoleInputOutput = new ConsoleInputOutput();
    }

    public void Dispose()
    {
        Console.SetOut(this.originalOutputWriter);
        this.mockedOutputWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void WriteByte()
    {
        this.consoleInputOutput.WriteByte(Convert.ToByte('a'));
        this.consoleInputOutput.WriteByte(Convert.ToByte('\n'));
        var output = this.mockedOutputWriter.GetStringBuilder();

        Assert.Equal("a\n", output.ToString());

        output.Clear();
    }

    [Fact]
    public void ReadByte()
    {
        var mockedInputReader = new StringReader("q");
        var originalInputReader = Console.In;
        Console.SetIn(mockedInputReader);

        try
        {
            var readed = this.consoleInputOutput.ReadByte();
            Assert.Equal(Convert.ToByte('q'), readed);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Console.SetIn(originalInputReader);
        }
    }
}
