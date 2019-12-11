namespace Yabft.Shared
{
    using System.Collections.Generic;
    using System.Linq;
    using static Yabft.Shared.Constants;

    public static class Parser
    {
        public static bool IsValid(string brainfuckProgram)
        {
            if (string.IsNullOrWhiteSpace(brainfuckProgram))
            {
                return false;
            }

            Stack<char> stack = new Stack<char>();
            foreach (char character in brainfuckProgram)
            {
                if (character == '[')
                {
                    stack.Push(character);
                }
                else if (character == ']')
                {
                    bool hasMatchingParenthesis = stack.Any();
                    if (!hasMatchingParenthesis)
                    {
                        return false;
                    }

                    stack.Pop();
                }
            }

            return stack.Count == 0;
        }

        public static Instruction[] Parse(string brainfuckProgram)
        {
            List<Instruction> ret = new List<Instruction>();
            int addAmount = 0;
            int moveAmount = 0;

            for (int i = 0; i < brainfuckProgram.Length; i++)
            {
                Instruction instruction = new Instruction(brainfuckProgram[i]);
                Instruction nextInstruction = new Instruction(InstructionType.Nop);
                if (i + 1 < brainfuckProgram.Length)
                {
                    nextInstruction = new Instruction(brainfuckProgram[i + 1]);
                }

                switch (instruction.Type)
                {
                    case InstructionType.Add:
                        addAmount += 1;
                        break;
                    case InstructionType.Substract:
                        addAmount -= 1;
                        break;
                    case InstructionType.MoveLeft:
                        moveAmount -= 1;
                        break;
                    case InstructionType.MoveRight:
                        moveAmount += 1;
                        break;
                    case InstructionType.Nop:
                        break;
                    default:
                        ret.Add(instruction);
                        break;
                }

                if (instruction.IsAddOrSubstract && !nextInstruction.IsAddOrSubstract)
                {
                    Instruction toAdd = GetAddOrSubstractInstruction(addAmount);
                    if (toAdd.Type != InstructionType.Nop)
                    {
                        ret.Add(toAdd);
                        addAmount = 0;
                    }
                }

                if (instruction.IsMove && !nextInstruction.IsMove)
                {
                    Instruction toAdd = GetMoveLeftOrRightInstruction(moveAmount);
                    if (toAdd.Type != InstructionType.Nop)
                    {
                        ret.Add(toAdd);
                        moveAmount = 0;
                    }
                }
            }

            return ret.ToArray();
        }

        private static Instruction GetAddOrSubstractInstruction(int addAmount)
        {
            switch (addAmount)
            {
                case var _ when addAmount > 0:
                    return new Instruction(InstructionType.Add, addAmount);
                case var _ when addAmount < 0:
                    return new Instruction(InstructionType.Substract, addAmount * -1);
                default:
                    return new Instruction(InstructionType.Nop);
            }
        }

        private static Instruction GetMoveLeftOrRightInstruction(int moveAmount)
        {
            switch (moveAmount)
            {
                case var _ when moveAmount > 0:
                    return new Instruction(InstructionType.MoveRight, moveAmount);
                case var _ when moveAmount < 0:
                    return new Instruction(InstructionType.MoveLeft, moveAmount * -1);
                default:
                    return new Instruction(InstructionType.Nop);
            }
        }
    }
}
