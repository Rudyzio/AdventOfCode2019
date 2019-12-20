using Day_18_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day18Solver_Tests
    {
        [Theory]
        [InlineData("Day18_Input/puzzle.input", 4668)]
        [InlineData("Day18_Input/test1.input", 8)]
        [InlineData("Day18_Input/test2.input", 86)]
        [InlineData("Day18_Input/test3.input", 132)]
        [InlineData("Day18_Input/test4.input", 136)]
        [InlineData("Day18_Input/test5.input", 81)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day18Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day18_Input/puzzle2.input", 101956)]
        [InlineData("Day18_Input/test6.input", 8)]
        [InlineData("Day18_Input/test7.input", 24)]
        [InlineData("Day18_Input/test8.input", 32)]
        [InlineData("Day18_Input/test9.input", 72)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day18Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
