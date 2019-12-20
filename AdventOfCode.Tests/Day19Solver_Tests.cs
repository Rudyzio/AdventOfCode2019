using Day_19_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day19Solver_Tests
    {
        [Theory]
        [InlineData("Day19_Input/puzzle.input", 50, 50, 129)]
        public void TestPart1Solution(string inputFile, int x, int y, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day19Solver.Part1Solution(input, x, y);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day19_Input/puzzle.input", 14040699)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day19Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
