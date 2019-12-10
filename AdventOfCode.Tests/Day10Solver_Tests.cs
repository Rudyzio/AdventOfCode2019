using Day_10_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day10Solver_Tests
    {
        [Theory]
        [InlineData("Day10_Input/puzzle.input", 260)]
        [InlineData("Day10_Input/test1.input", 8)]
        [InlineData("Day10_Input/test2.input", 33)]
        [InlineData("Day10_Input/test3.input", 35)]
        [InlineData("Day10_Input/test4.input", 41)]
        [InlineData("Day10_Input/test5.input", 210)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[][] asteroidMap = Helpers.ReadAsteroidMap($"../../../{inputFile}");

            // Act
            var result = Day10Solver.Part1Solution(asteroidMap);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day10_Input/puzzle.input", 608)]
        [InlineData("Day10_Input/test5.input", 802)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[][] asteroidMap = Helpers.ReadAsteroidMap($"../../../{inputFile}");

            // Act
            var result = Day10Solver.Part2Solution(asteroidMap);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
