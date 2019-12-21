using Common;
using System;

namespace Day_21_Solver
{
    public static class Day21Solver
    {
        public static long Part1Solution(long[] input)
        {
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            string[] commands = new string[]
            {
                "NOT A T\n",
                "OR T J\n",
                "NOT B T\n",
                "OR T J\n",
                "NOT C T\n",
                "OR T J\n",
                "AND D J\n",
                "WALK\n"
            };

            foreach (var command in commands)
            {
                foreach (var character in command)
                {
                    intCodeProgram.Input.Enqueue(character);
                }
            }

            intCodeProgram.Run();

            while (intCodeProgram.Output.Count > 1)
            {
                Console.Write((char)intCodeProgram.Output.Dequeue());
            }
            return intCodeProgram.Output.Dequeue();
        }

        public static long Part2Solution(long[] input)
        {
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            string[] commands = new string[]
            {
                "NOT A T\n",
                "OR T J\n",
                "NOT B T\n",
                "OR T J\n",
                "NOT C T\n",
                "OR T J\n",
                "AND D J\n", // Same as part 1
                "NOT H T\n",
                "NOT T T\n",
                "OR E T\n",
                "AND T J\n",
                "RUN\n"
            };

            foreach (var command in commands)
            {
                foreach (var character in command)
                {
                    intCodeProgram.Input.Enqueue(character);
                }
            }

            intCodeProgram.Run();

            while (intCodeProgram.Output.Count > 1)
            {
                Console.Write((char)intCodeProgram.Output.Dequeue());
            }
            return intCodeProgram.Output.Dequeue();
        }
    }
}
