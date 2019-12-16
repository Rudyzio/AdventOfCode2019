using Day_15_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day15Solver_Tests
    {
        [Theory]
        [InlineData("Day15_Input/puzzle.input", 300)]
        public void TestPart1Solution(string inputFile, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day15Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day15_Input/puzzle.input", 101956)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day15Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
