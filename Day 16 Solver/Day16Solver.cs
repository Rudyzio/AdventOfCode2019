using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_16_Solver
{
    public static class Day16Solver
    {
        public static int Part1Solution(int[] input)
        {
            int[] output = FFT(input, 100);

            string outputString = string.Empty;
            for (var i = 0; i < 8; i++)
            {
                outputString += output[i].ToString();
            }

            return int.Parse(outputString);
        }

        public static int Part2Solution(int[] input)
        {
            int[] expandedInput = RepeatTenThousandTimes(input);
            int[] firstSevenDigits = expandedInput.Take(7).ToArray();
            string offsetString = string.Empty;
            foreach (var digit in firstSevenDigits)
            {
                offsetString += digit.ToString();
            }
            int offset = int.Parse(offsetString);

            int[] output = OptimizedFFT(expandedInput, 100, offset);

            string outputString = string.Empty;
            for (var i = offset; i < offset + 8; i++)
            {
                outputString += output[i].ToString();
            }

            return int.Parse(outputString);
        }

        private static int[] FFT(int[] input, int phases)
        {
            List<int> basePattern = new List<int> { 0, 1, 0, -1 };
            int phase = 0;
            int[] output = new int[input.Length];

            while (phase < phases)
            {
                int iteration = 1;
                output = new int[input.Length];

                for (var j = 0; j < output.Length; j++)
                {
                    List<int> currentPattern = GetPattern(basePattern, iteration, input.Length);
                    var currentOutput = 0;
                    for (var i = 0; i < input.Length; i++)
                    {
                        currentOutput += (currentPattern[i] * input[i]);
                    }
                    output[j] = GetDigit(currentOutput, 1);
                    iteration++;
                }

                string printPhase = string.Empty;
                foreach (var entry in output)
                {
                    printPhase += $" {entry}";
                }
                Console.WriteLine(printPhase);

                input = output;
                phase++;
            }

            return output;
        }

        // Normal FFT was made to trick us in part 2...
        /*
         * First solution I tried:
         *      For phase 0, we begin with 0 zeros
         *      For phase 1, we begin with 1 zero
         *      For phase 2, we begin with 2 zeros
         *      
         *      For offset N, we begin with N zeros
         *      So until offset we just don't care about the results until there and calculate to front
         *      
         *      Still slow af...
         * 
         * This solution:
         *      Take final digit and apply pattern to it
         *      In this case, no matter the lenght of the pattern to apply will always look like 0,0,0...,0,1
         *      So last digit will always be the same (we can see that in the first example
         *      
         *      Second digit from the end.
         *      Pattern will look like 0,0,0...0,1,1
         *      So it will be the last digit of the sum between last digit and second from end digit (like inside the for)
         *      
         *      Funnily enough, this does not work for part 1 inputs!
         *  
         */
        private static int[] OptimizedFFT(int[] input, int phases, int offset)
        {
            int phase = 0;

            while (phase < phases)
            {
                for (int i = input.Length - 2; i >= offset; i--)
                {
                    int number = input[i + 1] + input[i];
                    input[i] = number % 10;
                }
                phase++;
            }

            return input;
        }

        private static List<int> GetPattern(List<int> basePattern, int times, int inputLength)
        {
            var toReturn = new List<int>();

            while (toReturn.Count - 1 != inputLength)
            {
                foreach (var value in basePattern)
                {
                    int count = 0;
                    while (count < times)
                    {
                        toReturn.Add(value);
                        count++;
                        if (toReturn.Count - 1 == inputLength)
                        {
                            break;
                        }
                    }
                    if (toReturn.Count - 1 == inputLength)
                    {
                        break;
                    }
                }
            }

            toReturn.RemoveAt(0);
            return toReturn;
        }

        private static int GetDigit(int value, int rightToLeftTimes)
        {
            int count = 0;
            int toReturn = value;
            while (count < rightToLeftTimes)
            {
                toReturn = value % 10;
                value /= 10;
                count++;
            }
            return Math.Abs(toReturn);
        }

        private static int[] RepeatTenThousandTimes(int[] input)
        {
            int[] output = Enumerable.Repeat(input, 10000).SelectMany(arr => arr).ToArray();
            return output;
        }
    }
}
