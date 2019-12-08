using Day_10_Solver;
using Day_11_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day11Solver_Tests
    {
        [Theory]
        [InlineData("Day11_Input/puzzle.input", 5357)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day11Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day11_Input/puzzle.input", 101956)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day11Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
