﻿using Day_25_Solver;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day25Solver_Tests
    {
        [Theory]
        [InlineData("Day25_Input/puzzle.input", 5357)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            long[] input = Helpers.ReadIntCodeInput($"../../../{inputFile}");

            // Act
            var result = Day25Solver.Part1Solution(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day25_Input/puzzle.input", 102556)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day25Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
