using Day_6_Solver;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day6Solver_Tests
    {
        [Theory]
        [InlineData("Day6_Input/test.input", 42)]
        [InlineData("Day6_Input/puzzle.input", 117672)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            List<KeyValuePair<string, string>> input = Helpers.ReadOrbits($"../../../{inputFile}");

            // Act
            var result = Day6Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day6_Input/test2.input", 4)]
        [InlineData("Day6_Input/puzzle.input", 277)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            List<KeyValuePair<string, string>> input = Helpers.ReadOrbits($"../../../{inputFile}");

            // Act
            var result = Day6Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
