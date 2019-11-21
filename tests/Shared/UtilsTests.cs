namespace Tests.Shared
{
    using Xunit;
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
        public void ToByte_NoWrap()
        {
            for (int i = 0; i < 255; i++)
            {
                Assert.Equal((byte)i, Yabft.Shared.Utils.ToByte(i));
            }
        }

        [Fact]
        public void ToByte_Wrap()
        {
            Assert.Equal(1, Yabft.Shared.Utils.ToByte(256));
        }

        [Fact]
        public void DecodeInstruction_InvalidInstruction()
        {
            Assert.Equal((Instruction.Nop, null), Yabft.Shared.Utils.DecodeInstruction('a'));
        }

        [Fact]
        public void DecodeInstruction_InstructionsParameters()
        {
            var instructionsWithoutParams = new char[] { '.', ',', '[', ']' };
            foreach (var item in instructionsWithoutParams)
            {
               Assert.Null(Yabft.Shared.Utils.DecodeInstruction(item).Item2);
            }
        }
    }
}
