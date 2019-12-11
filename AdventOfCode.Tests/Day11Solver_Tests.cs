using Day_10_Solver;
using Day_11_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day11Solver_Tests
    {
        [Theory]
        [InlineData("Day11_Input/puzzle.input", 2255)]
        public void TestPart1Solution(string inputFile, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day11Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day11_Input/puzzle.input", 248)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day11Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
