namespace Yabft.Shared
{
    public static class Constants
    {
        public static readonly int TapeLength = 30000;

        public enum InstructionType
        {
            Add,
            LoopBegin,
            LoopEnd,
            Move,
            Nop,
            Read,
            Write,
        }
    }
}
