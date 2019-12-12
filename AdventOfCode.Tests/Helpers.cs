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
            foreach (char digit in lines[0])
            {
                toReturn.Add(int.Parse(digit.ToString()));
            }
            return toReturn;
        }

        public static string[][] ReadAsteroidMap(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            string[][] toReturn = new string[lines.Length][];
            for (var i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                var newArray = new string[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    newArray[j] = line[j].ToString();
                }
                toReturn[i] = newArray;
            }
            return toReturn;
        }

        public static int[][] ReadJupiterMoons(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            int[][] toReturn = new int[lines.Length][];
            for (var i = 0; i < lines.Length; i++)
            {
                string[] splitted = lines[i].Split(",");
                var newArray = new int[splitted.Length];
                for (var j = 0; j < splitted.Length; j++)
                {
                    newArray[j] = int.Parse(splitted[j].Split("=")[1].Replace("<", "").Replace(">", ""));
                }
                toReturn[i] = newArray;
            }
            return toReturn;
        }
    }
}
