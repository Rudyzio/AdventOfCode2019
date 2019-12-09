using Common;
using System;

namespace Day_2_Solver
{
    public static class Day2Solver
    {
        public static long Part1Solution(long[] input)
        {
            var program = new IntCodeProgram(input);
            program.Run();
            return program.GetFirstPosition();
        }

        public static long Part2Solution(long[] originalNumbersArray)
        {
            var memoryNumbersArray = new long[originalNumbersArray.Length];

            bool found = false;
            long noun = 0;
            long verb = 0;
            for (var i = 0; i <= 99; i++)
            {
                for (var j = 0; j <= 99; j++)
                {
                    originalNumbersArray.CopyTo(memoryNumbersArray, 0);
                    memoryNumbersArray[1] = i;
                    memoryNumbersArray[2] = j;
                    IntCodeProgram intProgram = new IntCodeProgram(memoryNumbersArray);
                    intProgram.Run();

                    if (intProgram.GetFirstPosition() == 19690720)
                    {
                        noun = i;
                        verb = j;
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            return (100 * noun) + verb;
        }
    }
}
