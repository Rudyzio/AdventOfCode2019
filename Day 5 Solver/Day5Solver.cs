using Common;
using System;
using System.Linq;

namespace Day_5_Solver
{
    public static class Day5Solver
    {
        public static long Part1Solution(long[] input)
        {
            var program = new IntCodeProgram(input, 1);
            program.Run();
            return program.Output.Last();
        }

        public static long Part2Solution(long[] input)
        {
            var program = new IntCodeProgram(input, 5);
            program.Run();
            return program.Output.Last();
        }
    }
}
