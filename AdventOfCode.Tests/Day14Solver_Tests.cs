using Day_14_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day14Solver_Tests
    {
        [Theory]
        [InlineData("Day14_Input/puzzle.input", 783895)]
        [InlineData("Day14_Input/test1.input", 31)]
        [InlineData("Day14_Input/test2.input", 165)]
        [InlineData("Day14_Input/test3.input", 13312)]
        [InlineData("Day14_Input/test4.input", 180697)]
        [InlineData("Day14_Input/test5.input", 2210736)]
        public void TestPart1Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day14Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day14_Input/puzzle.input", 1896688)]
        [InlineData("Day14_Input/test3.input", 82892753)]
        [InlineData("Day14_Input/test4.input", 5586022)]
        [InlineData("Day14_Input/test5.input", 460664)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day14Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
