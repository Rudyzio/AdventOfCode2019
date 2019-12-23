using Day_23_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day23Solver_Tests
    {
        [Theory]
        [InlineData("Day23_Input/puzzle.input", 18513)]
        public void TestPart1Solution(string inputFile, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day23Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day23_Input/puzzle.input", 13286)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day23Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
