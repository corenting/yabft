namespace Yabft.InputOuput
{
    using System;

    public class FakeInputOutput : IInputOutput
    {
        public FakeInputOutput()
        {
            this.Output = string.Empty;
        }

        public string Output { get; private set; }

        public void WriteByte(byte b)
        {
            this.Output += Convert.ToChar(b);
        }

        public byte ReadByte()
        {
            return Convert.ToByte('a');
        }
    }
}
