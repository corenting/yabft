namespace Yabft.Shared
{
    using static Yabft.Shared.Constants;

    public class Instruction
    {
        public Instruction(InstructionType type)
        {
            this.Type = type;
        }

        public Instruction(char currentInstruction)
        {
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
    }
}
