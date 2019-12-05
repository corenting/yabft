namespace Tests.Shared
{
    using Xunit;

    public class ParserTests
    {
        [Theory]
        [InlineData("++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", true)]
        [InlineData("++++++++++>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", false)]
        [InlineData("++++++++++[>+++++++>++++++++++>+++>+<<<<->++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", false)]
        [InlineData("[", false)]
        [InlineData("]", false)]
        public void IsValid(string inputProgram, bool isValid)
        {
            Assert.Equal(isValid, Yabft.Shared.Parser.IsValid(inputProgram));
        }
    }
}
