namespace Tests.Common;

using Xunit;

public class MathUtilsTests
{
    [Fact]
    public void WrapNoWrap() => Assert.Equal(5, Yabft.Common.MathUtils.Wrap(5, -10, 10));

    [Fact]
    public void WrapMax() => Assert.Equal(-8, Yabft.Common.MathUtils.Wrap(5, -10, 3));

    [Fact]
    public void WrapSameAsMin() => Assert.Equal(1, Yabft.Common.MathUtils.Wrap(1, 1, 2));

    [Fact]
    public void WrapMaxIsWrapping() => Assert.Equal(1, Yabft.Common.MathUtils.Wrap(15, 1, 15));
}
