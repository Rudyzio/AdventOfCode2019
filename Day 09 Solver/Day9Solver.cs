using Common;

namespace Day_09_Solver
{
    public static class Day9Solver
    {
        public static long Part1Solution(long[] input, long inputValue)
        {
            IntCodeProgram program;
            if(inputValue == 0)
            {
                program = new IntCodeProgram(input);
            }
            else
            {
                program = new IntCodeProgram(input, inputValue);
            }
            var type = program.Run();
            return program.Output.Dequeue();
        }

        public static long Part2Solution(long[] input, long inputValue)
        {
            IntCodeProgram program;
            if (inputValue == 0)
            {
                program = new IntCodeProgram(input);
            }
            else
            {
                program = new IntCodeProgram(input, inputValue);
            }
            var type = program.Run();
            return program.Output.Dequeue();
        }
    }
}
