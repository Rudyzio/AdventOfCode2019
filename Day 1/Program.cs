using System;

namespace Day_1
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
            string[] lines = System.IO.File.ReadAllLines("../../../puzzle.input");

            int totalFuel = 0;
            foreach (string line in lines)
            {
                int mass = int.Parse(line);
                int individualFuel = Convert.ToInt32(Math.Floor((double)(mass / 3)) - 2);
                Console.WriteLine($"Fuel for {mass} is {individualFuel}");
                totalFuel += individualFuel;
            }

            Console.WriteLine($"Total fuel is {totalFuel}");
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void Part2Solution()
        {
            string[] lines = System.IO.File.ReadAllLines("../../../puzzle.input");

            int totalFuel = 0;
            foreach (string line in lines)
            {
                int mass = int.Parse(line);
                int individualFuel = 0;
                int intermediateMass = mass;
                string individualString = string.Empty;
                while (true)
                {
                    int intermediateFuel = Convert.ToInt32(Math.Floor((double)(intermediateMass / 3)) - 2);
                    if (intermediateFuel <= 0)
                    {
                        break;
                    }
                    individualString += string.IsNullOrEmpty(individualString) ? $"{intermediateFuel}" : $" + {intermediateFuel}";
                    individualFuel += intermediateFuel;
                    intermediateMass = intermediateFuel;
                }
                Console.WriteLine($"Fuel for {mass} is {individualFuel} --> {individualString}");
                totalFuel += individualFuel;
            }

            Console.WriteLine($"Total fuel is {totalFuel}");
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
