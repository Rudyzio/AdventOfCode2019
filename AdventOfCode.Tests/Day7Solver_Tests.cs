using Day_7_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day7Solver_Tests
    {
        [Theory]
        [InlineData("Day7_Input/puzzle.input", 95757)]
        [InlineData("Day7_Input/test1.input", 43210)]
        [InlineData("Day7_Input/test2.input", 54321)]
        [InlineData("Day7_Input/test3.input", 65210)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            int[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day7Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day7_Input/puzzle.input", 4275738)]
        [InlineData("Day7_Input/test4.input", 139629729)]
        [InlineData("Day7_Input/test5.input", 18216)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            int[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day7Solver.Part2Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
