using Day_12_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day12Solver_Tests
    {
        [Theory]
        [InlineData("Day12_Input/test1.input", 10, 179)]
        [InlineData("Day12_Input/test2.input", 100, 1940)]
        [InlineData("Day12_Input/puzzle.input", 1000, 10845)]
        public void TestPart1Solution(string inputFile, int steps, int expected)
        {
            // Arrange
            int[][] lines = Helpers.ReadJupiterMoons($"../../../{inputFile}");

            // Act
            var result = Day12Solver.Part1Solution(lines, steps);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day12_Input/test1.input", 2772)]
        [InlineData("Day12_Input/test2.input", 4686774924)]
        [InlineData("Day12_Input/puzzle.input", 551272644867044)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            int[][] lines = Helpers.ReadJupiterMoons($"../../../{inputFile}");

            // Act
            var result = Day12Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
