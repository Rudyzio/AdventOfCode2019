using Day_09_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    [Collection("Sequential")]

    public class Day09Solver_Tests
    {
        [Theory]
        [InlineData("Day09_Input/puzzle.input", 1, 2890527621)]
        [InlineData("Day09_Input/test1.input", 0, 109)]
        [InlineData("Day09_Input/test2.input", 0, 1219070632396864)]
        [InlineData("Day09_Input/test3.input", 0, 1125899906842624)]
        public void TestPart1Solution(string inputFile, long inputValue, long expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day9Solver.Part1Solution(input, inputValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day09_Input/puzzle.input", 2, 66772)]
        public void TestPart2Solution(string inputFile, long inputValue, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day9Solver.Part2Solution(input, inputValue);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
