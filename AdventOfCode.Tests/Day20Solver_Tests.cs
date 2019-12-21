using Day_20_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day20Solver_Tests
    {
        [Theory]
        [InlineData("Day20_Input/puzzle.input", 684)]
        [InlineData("Day20_Input/test1.input", 23)]
        [InlineData("Day20_Input/test2.input", 58)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day20Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day20_Input/test1.input", 26)]
        [InlineData("Day20_Input/test3.input", 396)]
        [InlineData("Day20_Input/puzzle.input", 7758)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day20Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
