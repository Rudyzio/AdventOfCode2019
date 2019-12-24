using Day_24_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day24Solver_Tests
    {
        [Theory]
        [InlineData("Day24_Input/puzzle.input", 17321586)]
        [InlineData("Day24_Input/test1.input", 2129920)]
        public void TestPart1Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day24Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day24_Input/puzzle.input", 200, 1921)]
        [InlineData("Day24_Input/test1.input", 10, 99)]
        public void TestPart2Solution(string inputFile, int minutes, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day24Solver.Part2Solution(lines, minutes);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
