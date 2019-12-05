namespace Tests.Shared
{
    using Xunit;

    public class MathUtilsTests
    {
        [Fact]
        public void Wrap_NoWrap()
        {
            Assert.Equal(5, Yabft.Shared.MathUtils.Wrap(5, -10, 10));
        }

        [Fact]
        public void Wrap_Max()
        {
            Assert.Equal(-8, Yabft.Shared.MathUtils.Wrap(5, -10, 3));
        }

        [Fact]
        public void Wrap_SameAsMin()
        {
            Assert.Equal(1, Yabft.Shared.MathUtils.Wrap(1, 1, 2));
        }

        [Fact]
        public void Wrap_MaxIsWrapping()
        {
            Assert.Equal(1, Yabft.Shared.MathUtils.Wrap(15, 1, 15));
        }
    }
}
