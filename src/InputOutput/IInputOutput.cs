namespace Yabft.InputOuput;

public interface IInputOutput
{
    void WriteByte(byte b);

    byte ReadByte();

    void OnStop();
}
