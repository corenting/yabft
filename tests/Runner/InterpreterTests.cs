namespace Tests.Interpreter
{
    using Xunit;
    using Yabft.InputOuput;
    using Yabft.Runner;

    public class InterpreterTests
    {
        [Theory]
        [InlineData("helloworld", "Hello World!\n")]
        [InlineData("bitwidth", "Hello World! 255\n")]
        public void Run_Program(string programName, string expectedOutput)
        {
            string helloWorld = Tests.Utils.LoadBrainFuckProgram(programName);

            FakeInputOutput fakeIo = new FakeInputOutput();
            AbstractRunner runner = new Interpreter(fakeIo, helloWorld);

            runner.Run();
            Assert.Equal(expectedOutput, fakeIo.Output);
        }
    }
}
