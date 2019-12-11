namespace Tests.Shared
{
    using Tests.Mocks;
    using Xunit;
    using Yabft.Runner;
    using Yabft.Shared;
    using static Yabft.Shared.Constants;

    public class InstructionTests
    {
        [Fact]
        public void Equals_Same()
        {
            Instruction first = new Instruction(InstructionType.MoveLeft, 5);
            Instruction second = new Instruction(InstructionType.MoveLeft, 5);
            Assert.Equal(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            Instruction first = new Instruction(InstructionType.MoveRight, 5);
            Instruction second = new Instruction(InstructionType.MoveLeft, 5);
            Assert.NotEqual(first, second);
        }

        [Fact]
        public void GetHashCode_Same()
        {
            Instruction first = new Instruction(InstructionType.MoveLeft, 5);
            Instruction second = new Instruction(InstructionType.MoveLeft, 5);
            Assert.Equal(first.GetHashCode(), second.GetHashCode());
        }

        [Fact]
        public void GetHashCode_Different()
        {
            Instruction first = new Instruction(InstructionType.MoveRight, 5);
            Instruction second = new Instruction(InstructionType.MoveLeft, 5);
            Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
        }
    }
}
