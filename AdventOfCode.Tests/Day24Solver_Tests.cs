using Day_24_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day24Solver_Tests
    {
        [Theory]
        [InlineData("Day24_Input/puzzle.input", 5357)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day24Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day24_Input/puzzle.input", 102456)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day24Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
