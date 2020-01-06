namespace Yabft.InputOuput
{
    using System;
    using System.Text;

    public class ConsoleInputOutput : IInputOutput
    {
        private StringBuilder buffer;

        public ConsoleInputOutput()
        {
            this.buffer = new StringBuilder();
        }

        public void WriteByte(byte b)
        {
            char newChar = Convert.ToChar(b);
            this.AddCharToBuffer(newChar);
            if (newChar == '\n')
            {
                this.OutputAndClearBuffer();
            }
        }

        public byte ReadByte()
        {
            char inputChar = Convert.ToChar(Console.Read());
            return Convert.ToByte(inputChar);
        }

        public void OnStop()
        {
            this.OutputAndClearBuffer();
        }

        private void AddCharToBuffer(char newChar)
        {
            this.buffer.Append(newChar);
        }

        private void OutputAndClearBuffer()
        {
            Console.Write(this.buffer);
            this.buffer.Clear();
        }
    }
}
