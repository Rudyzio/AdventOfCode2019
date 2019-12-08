using Day_23_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day23Solver_Tests
    {
        [Theory]
        [InlineData("Day23_Input/puzzle.input", 5357)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day23Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day23_Input/puzzle.input", 102356)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day23Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
