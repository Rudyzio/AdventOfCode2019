using Day_21_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day21Solver_Tests
    {
        [Theory]
        [InlineData("Day21_Input/puzzle.input", 19350258)]
        public void TestPart1Solution(string inputFile, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day21Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day21_Input/puzzle.input", 1142627861)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day21Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
