namespace Tests.Shared
{
    using System.Collections.Generic;
    using Xunit;
    using Yabft.Shared;

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

        [Fact]
        public void Parse_GroupAddSubstract()
        {
            string inputProgram = "++-++-++.";
            List<Instruction> parsedProgram = Yabft.Shared.Parser.Parse(inputProgram);

            List<Instruction> expectedProgram = new List<Instruction>()
            {
                new Instruction(Constants.InstructionType.Add, 4),
                new Instruction(Constants.InstructionType.Write, 0),
            };
            Assert.Equal(parsedProgram, expectedProgram);
        }

        [Fact]
        public void Parse_GroupMove()
        {
            string inputProgram = ".<<<>";
            List<Instruction> parsedProgram = Yabft.Shared.Parser.Parse(inputProgram);

            List<Instruction> expectedProgram = new List<Instruction>()
            {
                new Instruction(Constants.InstructionType.Write, 0),
                new Instruction(Constants.InstructionType.MoveLeft, 2),
            };
            Assert.Equal(parsedProgram, expectedProgram);
        }
    }
}
