namespace Yabft.Common;
using System.Collections.Generic;
using System.Linq;
using static Yabft.Common.Constants;

public static class Parser
{
    public static bool IsValid(string brainfuckProgram)
    {
        if (string.IsNullOrWhiteSpace(brainfuckProgram))
        {
            return false;
        }

        var stack = new Stack<char>();
        foreach (var character in brainfuckProgram)
        {
            if (character == '[')
            {
                stack.Push(character);
            }
            else if (character == ']')
            {
                var hasMatchingParenthesis = stack.Any();
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
        var ret = new List<Instruction>();
        var addAmount = 0;
        var moveAmount = 0;

        for (var i = 0; i < brainfuckProgram.Length; i++)
        {
            var instruction = new Instruction(brainfuckProgram[i]);
            var nextInstruction = new Instruction(InstructionType.Nop);
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
                case InstructionType.LoopBegin:
                case InstructionType.LoopEnd:
                case InstructionType.Read:
                case InstructionType.Write:
                default:
                    ret.Add(instruction);
                    break;
            }

            if (instruction.IsAddOrSubstract && !nextInstruction.IsAddOrSubstract)
            {
                var toAdd = GetAddOrSubstractInstruction(addAmount);
                if (toAdd.Type != InstructionType.Nop)
                {
                    ret.Add(toAdd);
                    addAmount = 0;
                }
            }

            if (instruction.IsMove && !nextInstruction.IsMove)
            {
                var toAdd = GetMoveLeftOrRightInstruction(moveAmount);
                if (toAdd.Type != InstructionType.Nop)
                {
                    ret.Add(toAdd);
                    moveAmount = 0;
                }
            }
        }

        return ret.ToArray();
    }

    private static Instruction GetAddOrSubstractInstruction(int addAmount) => addAmount switch
    {
        var _ when addAmount > 0 => new Instruction(InstructionType.Add, addAmount),
        var _ when addAmount < 0 => new Instruction(InstructionType.Substract, addAmount * -1),
        _ => new Instruction(InstructionType.Nop),
    };

    private static Instruction GetMoveLeftOrRightInstruction(int moveAmount) => moveAmount switch
    {
        var _ when moveAmount > 0 => new Instruction(InstructionType.MoveRight, moveAmount),
        var _ when moveAmount < 0 => new Instruction(InstructionType.MoveLeft, moveAmount * -1),
        _ => new Instruction(InstructionType.Nop),
    };
}
