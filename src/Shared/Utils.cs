namespace Yabft.Shared
{
    using System;
    using static Yabft.Shared.Constants;

    public static class Utils
    {
        public static Instruction DecodeInstruction(this char currentInstruction)
        {
            switch (currentInstruction)
            {
                case '>':
                    return new Instruction(InstructionType.Move, false);
                case '<':
                    return new Instruction(InstructionType.Move, true);
                case '+':
                    return new Instruction(InstructionType.Add, false);
                case '-':
                    return new Instruction(InstructionType.Add, true);
                case '.':
                    return new Instruction(InstructionType.Write);
                case ',':
                    return new Instruction(InstructionType.Read);
                case '[':
                    return new Instruction(InstructionType.LoopBegin);
                case ']':
                    return new Instruction(InstructionType.LoopEnd);
                default:
                    return new Instruction(InstructionType.Nop);
            }
        }

        public static byte ToByte(this int intValue)
        {
            return Convert.ToByte(intValue.Wrap(0, 255));
        }

        public static int Wrap(this int value, int min, int max)
        {
            return ((((value - min) % (max - min)) + (max - min)) % (max - min)) + min;
        }
    }
}
