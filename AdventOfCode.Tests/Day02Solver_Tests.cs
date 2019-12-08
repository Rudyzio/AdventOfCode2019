using Day_2_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day02Solver_Tests
    {
        [Theory]
        [InlineData("Day02_Input/test1.input", 3500)]
        [InlineData("Day02_Input/test2.input", 2)]
        [InlineData("Day02_Input/test3.input", 2)]
        [InlineData("Day02_Input/test4.input", 2)]
        [InlineData("Day02_Input/test5.input", 30)]
        [InlineData("Day02_Input/puzzle.input", 3058646)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            int[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");
            if (inputFile.Contains("puzzle.input"))
            {
                // Set position 1 and 2 values to IntCode computer error state 1202
                input[1] = 12;
                input[2] = 2;
            }

            // Act
            var result = Day2Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day02_Input/puzzle.input", 8976)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            int[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day2Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
