namespace Tests.Common;
using Xunit;
using Yabft.Common;
public class ParserTests
{
    [Theory]
    [InlineData("++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", true)]
    [InlineData("++++++++++>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", false)]
    [InlineData("++++++++++[>+++++++>++++++++++>+++>+<<<<->++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", false)]
    [InlineData("[", false)]
    [InlineData("]", false)]
    public void IsValid(string inputProgram, bool isValid) => Assert.Equal(isValid, Parser.IsValid(inputProgram));

    [Fact]
    public void ParseGroupAddSubstract()
    {
        var inputProgram = "++-++-++.";
        var parsedProgram = Parser.Parse(inputProgram);

        var expectedProgram = new Instruction[]
        {
                new(Constants.InstructionType.Add, 4),
                new(Constants.InstructionType.Write, 0),
        };
        Assert.Equal(parsedProgram, expectedProgram);
    }

    [Fact]
    public void ParseGroupMove()
    {
        var inputProgram = ".<<<>";
        var parsedProgram = Parser.Parse(inputProgram);

        var expectedProgram = new Instruction[]
        {
                new(Constants.InstructionType.Write, 0),
                new(Constants.InstructionType.MoveLeft, 2),
        };
        Assert.Equal(parsedProgram, expectedProgram);
    }
}
