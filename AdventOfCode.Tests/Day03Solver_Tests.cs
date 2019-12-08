using Day_3_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day03Solver_Tests
    {
        [Theory]
        [InlineData("Day03_Input/test1.input", 6)]
        [InlineData("Day03_Input/test2.input", 159)]
        [InlineData("Day03_Input/test3.input", 135)]
        [InlineData("Day03_Input/puzzle.input", 5357)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day3Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day03_Input/test1.input", 30)]
        [InlineData("Day03_Input/test2.input", 610)]
        [InlineData("Day03_Input/test3.input", 410)]
        [InlineData("Day03_Input/puzzle.input", 101956)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day3Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
