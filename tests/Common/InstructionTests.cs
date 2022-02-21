namespace Tests.Common;
using Xunit;
using Yabft.Common;
using static Yabft.Common.Constants;

public class InstructionTests
{
    [Fact]
    public void EqualsSame()
    {
        var first = new Instruction(InstructionType.MoveLeft, 5);
        var second = new Instruction(InstructionType.MoveLeft, 5);
        Assert.Equal(first, second);
    }

    [Fact]
    public void EqualsDifferent()
    {
        var first = new Instruction(InstructionType.MoveRight, 5);
        var second = new Instruction(InstructionType.MoveLeft, 5);
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void GetHashCodeSame()
    {
        var first = new Instruction(InstructionType.MoveLeft, 5);
        var second = new Instruction(InstructionType.MoveLeft, 5);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void GetHashCodeDifferent()
    {
        var first = new Instruction(InstructionType.MoveRight, 5);
        var second = new Instruction(InstructionType.MoveLeft, 5);
        Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
    }
}
