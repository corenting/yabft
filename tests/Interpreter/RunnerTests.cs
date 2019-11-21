namespace Tests.Interpreter
{
    using Xunit;
    using Yabft.InputOuput;
    using Yabft.Interpreter;

    public class RunnerTests
    {
        [Fact]
        public void Run_HelloWorld()
        {
            string helloWorld = Tests.Utils.LoadBrainFuckProgram("helloworld");

            FakeInputOutput fakeIo = new FakeInputOutput();
            Runner runner = new Runner(fakeIo, helloWorld);

            runner.Run();
            Assert.Equal("Hello World!\n", fakeIo.Output);
        }
    }
}
