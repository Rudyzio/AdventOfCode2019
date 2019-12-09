using System;
using System.Collections.Generic;

namespace AdventOfCode.Tests
{
    internal static class Helpers
    {
        public static long[] ReadIntCodeInput(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            return Array.ConvertAll(lines[0].Split(","), s => long.Parse(s));
        }

        public static List<KeyValuePair<string, string>> ReadOrbits(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            var toReturn = new List<KeyValuePair<string, string>>();
            foreach (var line in lines)
            {
                string[] splitted = line.Split(")");
                toReturn.Add(new KeyValuePair<string, string>(splitted[0], splitted[1]));
            }
            return toReturn;
        }

        public static List<int> ReadDigits(string input)
        {
            var toReturn = new List<int>();
            string[] lines = System.IO.File.ReadAllLines(input);
            foreach(char digit in lines[0])
            {
                toReturn.Add(int.Parse(digit.ToString()));
            }
            return toReturn;
        }
    }
}
