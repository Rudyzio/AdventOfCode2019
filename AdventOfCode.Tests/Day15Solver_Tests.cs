using Day_15_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day15Solver_Tests
    {
        [Theory]
        [InlineData("Day15_Input/puzzle.input", 5357)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day15Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day15_Input/puzzle.input", 101956)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day15Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
