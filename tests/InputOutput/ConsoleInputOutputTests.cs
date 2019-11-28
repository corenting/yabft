namespace Tests.InputOutput
{
    using System;
    using System.IO;
    using System.Text;
    using Xunit;
    using Yabft.InputOuput;

    public class ConsoleInputOutputTests : IDisposable
    {
        private ConsoleInputOutput consoleInputOutput;

        private StringWriter mockedOutputWriter;
        private TextWriter originalOutputWriter;

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
        }

        [Fact]
        public void WriteByte()
        {
            this.consoleInputOutput.WriteByte(Convert.ToByte(97));
            StringBuilder output = this.mockedOutputWriter.GetStringBuilder();

            Assert.Equal("a", output.ToString());

            output.Clear();
        }

        [Fact]
        public void ReadByte()
        {
            StringReader mockedInputReader = new StringReader("q");
            TextReader originalInputReader = Console.In;
            Console.SetIn(mockedInputReader);

            try
            {
                byte readed = this.consoleInputOutput.ReadByte();
                Assert.Equal(Convert.ToByte('q'), readed);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Console.SetIn(originalInputReader);
            }
        }
    }
}
