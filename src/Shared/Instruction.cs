namespace Yabft.Shared
{
    using static Yabft.Shared.Constants;

    public class Instruction
    {
        public Instruction(InstructionType type, bool down = false)
        {
            this.Type = type;
            this.Down = down;
        }

        public InstructionType Type { get; private set; }

        public bool Down { get; private set; }
    }
}
