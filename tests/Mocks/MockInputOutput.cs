namespace Tests.Mocks;

using System;
using Yabft.InputOuput;

public class MockInputOutput : IInputOutput
{
    public MockInputOutput() => this.Output = string.Empty;

    public string Output { get; private set; }

    public void WriteByte(byte b) => this.Output += Convert.ToChar(b);

    public byte ReadByte() => Convert.ToByte('a');

    public void OnStop()
    {
    }
}
