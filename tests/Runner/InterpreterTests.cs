namespace Tests.Interpreter;

using Tests.Mocks;
using Xunit;
using Yabft.Common;
using Yabft.Runner;
public class InterpreterTests
{
    [Theory]
    [InlineData("helloworld", "Hello World!\n", true)]
    [InlineData("bitwidth", "Hello World! 255\n", true)]
    [InlineData("helloworld", "Hello World!\n", false)]
    [InlineData("bitwidth", "Hello World! 255\n", false)]
    public void RunProgram(string programName, string expectedOutput, bool enableWrapping)
    {
        var helloWorld = Utils.LoadBrainFuckProgram(programName);

        var fakeIo = new MockInputOutput();
        var options = new RunnerOptions
        (
            enableWrapping,
            fakeIo
        );
        AbstractRunner runner = new Interpreter(options, helloWorld);

        runner.Run();
        Assert.Equal(expectedOutput, fakeIo.Output);
    }
}
