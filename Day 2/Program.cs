using System;

namespace Day_2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Part1Solution();
            Part2Solution();
        }

        private static void Part1Solution()
        {
            var numbersArray = ReadInput("../../../puzzle.input");

            // Set position 1 and 2 values to IntCode computer error state 1202
            numbersArray[1] = 12;
            numbersArray[2] = 2;

            RunProgram(numbersArray);

            Console.WriteLine($"Result for first position is {numbersArray[0]}");
        }

        private static void Part2Solution()
        {
            var originalNumbersArray = ReadInput("../../../puzzle.input");
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
                    RunProgram(memoryNumbersArray);

                    if (memoryNumbersArray[0] == 19690720)
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

            Console.WriteLine($"Noun is {noun} and verb is {verb}. The answer for 100 * noun + verb is {100 * noun + verb}");
        }

        private static int[] ReadInput(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            return Array.ConvertAll(lines[0].Split(","), s => int.Parse(s));
        }

        private static void RunProgram(int[] memoryNumbersArray)
        {
            int instructionPointer = 0;
            while (memoryNumbersArray[instructionPointer] != 99)
            {
                var firstInputPosition = memoryNumbersArray[instructionPointer + 1];
                var secondInputPosition = memoryNumbersArray[instructionPointer + 2];
                var outputPosition = memoryNumbersArray[instructionPointer + 3];

                switch (memoryNumbersArray[instructionPointer])
                {
                    case 1:
                        memoryNumbersArray[outputPosition] = memoryNumbersArray[firstInputPosition] + memoryNumbersArray[secondInputPosition];
                        break;
                    case 2:
                        memoryNumbersArray[outputPosition] = memoryNumbersArray[firstInputPosition] * memoryNumbersArray[secondInputPosition];
                        break;
                    default:
                        throw new Exception("Not valid opCode");
                }

                instructionPointer += 4;
            }
        }
    }
}
