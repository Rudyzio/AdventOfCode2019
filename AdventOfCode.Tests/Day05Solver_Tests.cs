using Day_5_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day05Solver_Tests
    {
        [Theory]
        [InlineData("Day05_Input/puzzle.input", 5182797)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day5Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day05_Input/puzzle.input", 12077198)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day5Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
