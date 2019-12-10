namespace Tests.Interpreter
{
    using Tests.Mocks;
    using Xunit;
    using Yabft.Runner;

    public class InterpreterTests
    {
        [Theory]
        [InlineData("helloworld", "Hello World!\n")]
        [InlineData("bitwidth", "Hello World! 255\n")]
        public void Run_Program(string programName, string expectedOutput)
        {
            string helloWorld = Tests.Utils.LoadBrainFuckProgram(programName);

            MockInputOutput fakeIo = new MockInputOutput();
            AbstractRunner runner = new Interpreter(fakeIo, helloWorld);

            runner.Run();
            Assert.Equal(expectedOutput, fakeIo.Output);
        }
    }
}
