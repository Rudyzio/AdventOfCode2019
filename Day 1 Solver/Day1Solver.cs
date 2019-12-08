using System;

namespace Day_1_Solver
{
    public static class Day1Solver
    {
        public static int Part1Solution(string[] lines)
        {
            int totalFuel = 0;
            foreach (string line in lines)
            {
                int mass = int.Parse(line);
                int individualFuel = Convert.ToInt32(Math.Floor((double)(mass / 3)) - 2);
                totalFuel += individualFuel;
            }

            return totalFuel;
        }

        public static int Part2Solution(string[] lines)
        {
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
                totalFuel += individualFuel;
            }

            return totalFuel;
        }
    }
}
