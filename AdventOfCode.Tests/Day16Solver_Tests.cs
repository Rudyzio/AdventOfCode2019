using Day_16_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day16Solver_Tests
    {
        [Theory]
        [InlineData("Day16_Input/puzzle.input", 30369587)]
        [InlineData("Day16_Input/test1.input", 23845678)]
        [InlineData("Day16_Input/test2.input", 24176176)]
        [InlineData("Day16_Input/test3.input", 73745418)]
        [InlineData("Day16_Input/test4.input", 52432133)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            int[] lines = Helpers.ReadDigits($"../../../{inputFile}").ToArray();

            // Act
            var result = Day16Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day16_Input/puzzle.input", 27683551)]
        [InlineData("Day16_Input/test5.input", 84462026)]
        [InlineData("Day16_Input/test6.input", 78725270)]
        [InlineData("Day16_Input/test7.input", 53553731)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            int[] lines = Helpers.ReadDigits($"../../../{inputFile}").ToArray();

            // Act
            var result = Day16Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
