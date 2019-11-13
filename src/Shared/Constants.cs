namespace yabft.Shared
{
    static class Constants
    {
        public static readonly int TapeLength = 30000;

        public enum Instruction
        {
            Add,
            LoopBegin,
            LoopEnd,
            Move,
            Nop,
            Read,
            Write,
        }

        public enum InstructionParameter
        {
            Down,
            Up
        }
    }
}
