using Common;
using System;
using System.Linq;

namespace Day_5_Solver
{
    public static class Day5Solver
    {
        public static int Part1Solution(int[] input)
        {
            var program = new IntCodeProgram(input, 1);
            program.Run();
            return program.Output.Last();
        }

        public static int Part2Solution(int[] input)
        {
            var program = new IntCodeProgram(input, 5);
            program.Run();
            return program.Output.Last();
        }
    }
}
