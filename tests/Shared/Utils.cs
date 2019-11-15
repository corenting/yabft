namespace Tests.Shared
{
    using System;
    using Xunit;

    public class Utils
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
    }
}
