using Day_13_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day13Solver_Tests
    {
        [Theory]
        [InlineData("Day13_Input/puzzle.input", 355)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day13Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day13_Input/puzzle.input", 18371)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day13Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
