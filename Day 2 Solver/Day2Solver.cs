using Common;
using System;

namespace Day_2_Solver
{
    public static class Day2Solver
    {
        public static int Part1Solution(int[] input)
        {
            var program = new IntCodeProgram(input);
            program.Run();
            return program.GetFirstPosition();
        }

        public static int Part2Solution(int[] originalNumbersArray)
        {
            var memoryNumbersArray = new int[originalNumbersArray.Length];

            bool found = false;
            int noun = 0;
            int verb = 0;
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
