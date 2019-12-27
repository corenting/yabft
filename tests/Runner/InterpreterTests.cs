namespace Tests.Interpreter
{
    using Tests.Mocks;
    using Xunit;
    using Yabft.Runner;
    using Yabft.Shared;

    public class InterpreterTests
    {
        [Theory]
        [InlineData("helloworld", "Hello World!\n", true)]
        [InlineData("bitwidth", "Hello World! 255\n", true)]
        [InlineData("helloworld", "Hello World!\n", false)]
        [InlineData("bitwidth", "Hello World! 255\n", false)]
        public void Run_Program(string programName, string expectedOutput, bool enableWrapping)
        {
            string helloWorld = Tests.Utils.LoadBrainFuckProgram(programName);

            MockInputOutput fakeIo = new MockInputOutput();
            RunnerOptions options = new RunnerOptions
            {
                InputOutputSystem = fakeIo,
                Wrap = enableWrapping,
            };
            AbstractRunner runner = new Interpreter(options, helloWorld);

            runner.Run();
            Assert.Equal(expectedOutput, fakeIo.Output);
        }
    }
}
