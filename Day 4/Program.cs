using System;

namespace Day_4
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
            const int lowEnd = 138307;
            const int highEnd = 654504;

            int value = lowEnd;
            int count = 0;

            while (value < highEnd)
            {
                if (IsSixDigitNumber(value) && HasAdjacentDigits(value) && DigitNeverDecrease(value))
                {
                    count++;
                }
                value++;
            }
            Console.WriteLine($"There are {count} possibilities of a password");
        }

        private static void Part2Solution()
        {
            const int lowEnd = 138307;
            const int highEnd = 654504;

            int value = lowEnd;
            int count = 0;

            while (value < highEnd)
            {
                if (IsSixDigitNumber(value) && HasAdjacentDigitsNoLargerGroup(value) && DigitNeverDecrease(value))
                {
                    count++;
                }
                value++;
            }
            Console.WriteLine($"There are {count} possibilities of a password");
        }

        private static bool IsSixDigitNumber(int value)
        {
            return value.ToString().Length == 6;
        }

        private static bool HasAdjacentDigits(int value)
        {
            string valueString = value.ToString();
            for (var i = 0; i + 1 < valueString.Length; i++)
            {
                if (valueString[i] == valueString[i + 1])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DigitNeverDecrease(int value)
        {
            string valueString = value.ToString();
            for (var i = 0; i + 1 < valueString.Length; i++)
            {
                if (int.Parse(valueString[i].ToString()) > int.Parse(valueString[i + 1].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool HasAdjacentDigitsNoLargerGroup(int value)
        {
            string valueString = value.ToString();
            bool toReturn = false;
            char lastCharacter = '\0';
            for (var i = 0; i + 1 < valueString.Length; i++)
            {
                if (valueString[i] == valueString[i + 1] && valueString[i] != lastCharacter)
                {
                    if (i + 2 < valueString.Length && valueString[i] == valueString[i + 2])
                    {
                        lastCharacter = valueString[i];
                    }
                    else
                    {
                        toReturn = true;
                    }
                }
            }
            return toReturn;
        }
    }
}
