using Day_17_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day17Solver_Tests
    {
        [Theory]
        [InlineData("Day17_Input/puzzle.input", 3336)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day17Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day17_Input/puzzle.input", 597517)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day17Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
