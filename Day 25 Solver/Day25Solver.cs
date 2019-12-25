using Common;
using System;

namespace Day_25_Solver
{
    public static class Day25Solver
    {
        public static int Part1Solution(long[] input)
        {
            /*
             * Items needed:
             *  - Easter egg
             *  - Mug
             *  - Sand
             *  - Space heater
             */

            IntCodeProgram intCodeProgram = new IntCodeProgram(input);
            while (true)
            {
                string command = Console.ReadLine() + "\n";
                foreach (var character in command)
                {
                    intCodeProgram.Input.Enqueue(character);
                }

                var halt = intCodeProgram.Run();

                while (intCodeProgram.Output.Count > 0)
                {
                    Console.Write((char)intCodeProgram.Output.Dequeue());
                }
            }

            return 0;
        }

        public static int Part2Solution(string[] input)
        {
            return 0;
        }
    }
}
