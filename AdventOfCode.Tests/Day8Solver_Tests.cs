using Day_8_Solver;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day8Solver_Tests
    {
        [Theory]
        [InlineData("Day8_Input/puzzle.input", 25, 6, "1360")]
        [InlineData("Day8_Input/test.input", 3, 2, "1")]
        public void TestPart1Solution(string inputFile, int wide, int tall, string expected)
        {
            // Arrange
            List<int> input = Helpers.ReadDigits($"../../../{inputFile}");

            // Act
            var result = Day8Solver.Part1Solution(input, wide, tall);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // FPUAR
        [InlineData("Day8_Input/puzzle.input", 25, 6, "#### ###  #  #  ##  ###  #    #  # #  # #  # #  # ###  #  # #  # #  # #  # #    ###  #  # #### ###  #    #    #  # #  # # #  #    #     ##  #  # #  # ")]
        [InlineData("Day8_Input/test1.input", 2, 2, " ## ")]
        public void TestPart2Solution(string inputFile, int wide, int tall, string expected)
        {
            // Arrange
            List<int> input = Helpers.ReadDigits($"../../../{inputFile}");

            // Act
            var result = Day8Solver.Part2Solution(input, wide, tall);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
