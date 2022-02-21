namespace Yabft.Common;

using static Yabft.Common.Constants;

public class Instruction
{
    public Instruction(InstructionType type, int amount = 0)
    {
        this.Type = type;
        this.Amount = amount;
    }

    public Instruction(char currentInstruction, int amount = 0)
    {
        this.Amount = amount;
        this.Type = currentInstruction switch
        {
            '>' => InstructionType.MoveRight,
            '<' => InstructionType.MoveLeft,
            '+' => InstructionType.Add,
            '-' => InstructionType.Substract,
            '.' => InstructionType.Write,
            ',' => InstructionType.Read,
            '[' => InstructionType.LoopBegin,
            ']' => InstructionType.LoopEnd,
            _ => InstructionType.Nop,
        };
    }

    public InstructionType Type { get; private set; }

    public int Amount { get; private set; }

    public bool IsAddOrSubstract => this.Type is InstructionType.Add or InstructionType.Substract;

    public bool IsMove => this.Type is InstructionType.MoveLeft or InstructionType.MoveRight;

    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            var otherInstruction = (Instruction)obj;
            return this.Type == otherInstruction.Type && this.Amount == otherInstruction.Amount;
        }
    }

    public override int GetHashCode() => new { this.Type, this.Amount }.GetHashCode();
}
