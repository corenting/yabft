namespace Tests.Shared
{
    using Xunit;
    using Yabft.Shared;
    using static Yabft.Shared.Constants;

    public class UtilsTests
    {
        [Fact]
        public void Wrap_NoWrap()
        {
            Assert.Equal(5, Yabft.Shared.Utils.Wrap(5, -10, 10));
        }

        [Fact]
        public void Wrap_Max()
        {
            Assert.Equal(-8, Yabft.Shared.Utils.Wrap(5, -10, 3));
        }

        [Fact]
        public void Wrap_SameAsMin()
        {
            Assert.Equal(1, Yabft.Shared.Utils.Wrap(1, 1, 2));
        }

        [Fact]
        public void Wrap_MaxIsWrapping()
        {
            Assert.Equal(1, Yabft.Shared.Utils.Wrap(15, 1, 15));
        }

        [Fact]
        public void DecodeInstruction_InvalidInstruction()
        {
            Instruction decodedInstruction = Yabft.Shared.Utils.DecodeInstruction('a');
            Assert.Equal(InstructionType.Nop, decodedInstruction.Type);
        }
    }
}
