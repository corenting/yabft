namespace Yabft.InputOuput
{
    using System;

    public class ConsoleInputOutput : IInputOutput
    {
        public void WriteByte(byte b)
        {
            Console.Write(Convert.ToChar(b));
        }

        public byte ReadByte()
        {
            char inputChar = Console.ReadKey().KeyChar;
            return Convert.ToByte(inputChar);
        }
    }
}
