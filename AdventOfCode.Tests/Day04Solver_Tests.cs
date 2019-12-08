using Day_4_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day04Solver_Tests
    {
        [Theory]
        [InlineData(1855)]
        public void TestPart1Solution(int expected)
        {
            // Arrange

            // Act
            var result = Day4Solver.Part1Solution();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1253)]
        public void TestPart2Solution(int expected)
        {
            // Arrange

            // Act
            var result = Day4Solver.Part2Solution();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
