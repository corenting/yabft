namespace Yabft.Common;

public static class Constants
{
    public static readonly int TapeLength = 30000;

    public enum InstructionType
    {
        Add,
        LoopBegin,
        LoopEnd,
        MoveLeft,
        MoveRight,
        Nop,
        Read,
        Substract,
        Write,
    }
}
