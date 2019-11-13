using System;
using static yabft.Shared.Constants;

namespace yabft.Shared
{
    static class Utils
    {
        public static (Instruction, InstructionParameter?) DecodeInstruction(this char currentInstruction)
        {
            switch (currentInstruction)
            {
                case '>':
                    return (Instruction.Move, InstructionParameter.Up);
                case '<':
                    return (Instruction.Move, InstructionParameter.Down);
                case '+':
                    return (Instruction.Add, InstructionParameter.Up);
                case '-':
                    return (Instruction.Add, InstructionParameter.Down);
                case '.':
                    return (Instruction.Write, null);
                case ',':
                    return (Instruction.Read, null);
                case '[':
                    return (Instruction.LoopBegin, null);
                case ']':
                    return (Instruction.LoopEnd, null);
                default:
                    return (Instruction.Nop, null);
            }
        }

        public static byte ToByte(this int intValue)
        {
            return Convert.ToByte(intValue.Wrap(0, 255));
        }

        public static int Wrap(this int value, int min, int max)
        {
            return (((value - min) % (max - min)) + (max - min)) % (max - min) + min;
        }
    }
}
