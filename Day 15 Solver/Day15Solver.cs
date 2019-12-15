using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_15_Solver
{
    public static class Day15Solver
    {
        public static long Part1Solution(long[] input)
        {
            return TraverseGrid(input);
        }

        private static int TraverseGrid(long[] input)
        {
            List<Position> exploredPositions = new List<Position>();
            Queue<Droid> droids = new Queue<Droid>();
            exploredPositions.Add(new Position(0, 0, Answer.Success));
            var initialDroid = new Droid(new Position(0, 0, Answer.Allowed), new IntCodeProgram(input));
            droids.Enqueue(initialDroid);

            while (droids.Count > 0)
            {
                var droid = droids.Dequeue();

                // There are four directions
                for (int i = 1; i <= 4; i++)
                {
                    var command = (Command)i;
                    var newDroid = droid.MoveBFS(command, exploredPositions);

                    if (newDroid != null)
                    {
                        if (newDroid.Success)
                        {
                            return 100; // Just dummy to see if it really finds anything
                        }
                        droids.Enqueue(newDroid);
                    }
                }

                if(exploredPositions.Count > 2000)
                {
                    break;
                }
            }
            PrintMap(exploredPositions);
            return 0;
        }

        public static int Part2Solution(long[] input)
        {
            return 0;
        }

        private static void PrintMap(List<Position> exploredPositions)
        {
            int minY = Math.Abs(exploredPositions.Min(mark => mark.Y));
            int minX = Math.Abs(exploredPositions.Min(mark => mark.X));

            int maxY = exploredPositions.Max(mark => mark.Y);
            int maxX = exploredPositions.Max(mark => mark.X);

            int yLength = minY + maxY;
            int xLength = minX + maxX;

            exploredPositions = exploredPositions.Select(item => new Position(item.X + minX, item.Y + minY, item.Content)).ToList();

            string[][] matrix = new string[yLength + 2][];

            for (var i = 0; i < yLength + 2; i++)
            {
                var newArray = new string[xLength + 2];
                for (var j = 0; j < xLength + 2; j++)
                {
                    newArray[j] = ".";
                }
                matrix[i] = newArray;
            }

            int test = 0;
            foreach (var mark in exploredPositions)
            {
                test++;
                switch(mark.Content)
                {
                    case Answer.Allowed:
                        matrix[mark.Y][mark.X] = ".";
                        break;
                    case Answer.Wall:
                        matrix[mark.Y][mark.X] = "#";
                        break;
                    case Answer.Success:
                        matrix[mark.Y][mark.X] = "S";
                        break;
                }
            }

            for (var i = matrix.Length - 1; i >= 0; i--)
            {
                var toPrint = string.Empty;
                foreach (var character in matrix[i])
                {
                    toPrint += character;
                }
                Console.WriteLine(toPrint);
            }
        }
    }
}
