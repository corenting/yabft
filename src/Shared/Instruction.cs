namespace Yabft.Shared
{
    using static Yabft.Shared.Constants;

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
            switch (currentInstruction)
            {
                case '>':
                    this.Type = InstructionType.MoveRight;
                    break;
                case '<':
                    this.Type = InstructionType.MoveLeft;
                    break;
                case '+':
                    this.Type = InstructionType.Add;
                    break;
                case '-':
                    this.Type = InstructionType.Substract;
                    break;
                case '.':
                    this.Type = InstructionType.Write;
                    break;
                case ',':
                    this.Type = InstructionType.Read;
                    break;
                case '[':
                    this.Type = InstructionType.LoopBegin;
                    break;
                case ']':
                    this.Type = InstructionType.LoopEnd;
                    break;
                default:
                    this.Type = InstructionType.Nop;
                    break;
            }
        }

        public InstructionType Type { get; private set; }

        public int Amount { get; private set; }

        public bool IsAddOrSubstract
        {
            get => this.Type == InstructionType.Add || this.Type == InstructionType.Substract;
        }

        public bool IsMove
        {
            get => this.Type == InstructionType.MoveLeft || this.Type == InstructionType.MoveRight;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Instruction otherInstruction = (Instruction)obj;
                return this.Type == otherInstruction.Type && this.Amount == otherInstruction.Amount;
            }
        }

        public override int GetHashCode()
        {
            return new { this.Type, this.Amount }.GetHashCode();
        }
    }
}
