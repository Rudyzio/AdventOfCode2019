using Day_22_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day22Solver_Tests
    {
        [Theory]
        [InlineData("Day22_Input/puzzle.input", 10007, 2019, 5169)]
        [InlineData("Day22_Input/test1.input", 10, 5, 4)]
        [InlineData("Day22_Input/test2.input", 10, 5, 2)]
        [InlineData("Day22_Input/test3.input", 10, 5, 9)]
        [InlineData("Day22_Input/test4.input", 10, 5, 5)]
        [InlineData("Day22_Input/test5.input", 10, 5, 5)]
        [InlineData("Day22_Input/test6.input", 10, 5, 6)]
        [InlineData("Day22_Input/test7.input", 10, 5, 7)]
        [InlineData("Day22_Input/test8.input", 10, 5, 2)]
        public void TestPart1Solution(string inputFile, int deckSize, int numberPosition, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day22Solver.Part1Solution(lines, deckSize, numberPosition);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day22_Input/puzzle.input", 119315717514047, 101741582076661, 2020, 74258074061935)]
        public void TestPart2Solution(string inputFile, long deckSize, long shuffleAmount, int cardPosition, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day22Solver.Part2Solution(lines, deckSize, shuffleAmount, cardPosition);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
