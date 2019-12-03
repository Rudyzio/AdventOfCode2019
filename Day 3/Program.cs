using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_3
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

            string[] firstWireInput = lines[0].Split(",");
            string[] secondWireOutput = lines[1].Split(",");

            List<GridMark> firstWireMarks = CreateGridMarks(firstWireInput);
            List<GridMark> secondWireMarks = CreateGridMarks(secondWireOutput);

            var intersections = FetchIntersections(firstWireMarks, secondWireMarks);
            int closestPort = 0;

            Console.WriteLine("### These are the intersection points ###");
            foreach (var mark in intersections)
            {
                if (closestPort == 0)
                {
                    closestPort = mark.ManhattanDistance();
                }
                else
                {
                    closestPort = closestPort > mark.ManhattanDistance() ? mark.ManhattanDistance() : closestPort;
                }
            }

            Console.WriteLine($"Closest port distance is {closestPort}");
        }

        private static void Part2Solution()
        {
            string[] lines = System.IO.File.ReadAllLines("../../../puzzle.input");

            string[] firstWireInput = lines[0].Split(",");
            string[] secondWireOutput = lines[1].Split(",");

            List<GridMark> firstWireMarks = CreateGridMarks(firstWireInput);
            List<GridMark> secondWireMarks = CreateGridMarks(secondWireOutput);

            var intersections = FetchIntersections(firstWireMarks, secondWireMarks);
            int lessSteps = 0;

            foreach (var intersection in intersections)
            {
                GridMark mark1 = firstWireMarks.First(item => item.X == intersection.X && item.Y == intersection.Y);
                GridMark mark2 = secondWireMarks.First(item => item.X == intersection.X && item.Y == intersection.Y);

                if (lessSteps == 0)
                {
                    lessSteps = mark1.Steps + mark2.Steps;
                }
                else
                {
                    lessSteps = lessSteps > mark1.Steps + mark2.Steps ? mark1.Steps + mark2.Steps : lessSteps;
                }
            }

            Console.WriteLine($"Closest step distance is {lessSteps}");
        }

        private static List<GridMark> CreateGridMarks(string[] wireInput)
        {
            List<GridMark> wireMarks = new List<GridMark>();

            GridMark currentPosition = new GridMark(0, 0, 0);
            foreach (var instruction in wireInput)
            {
                char direction = instruction[0];
                int nSteps = int.Parse(instruction.Substring(1));

                switch (direction)
                {
                    case 'R':
                        for (var i = 0; i < nSteps; i++)
                        {
                            currentPosition.X++;
                            currentPosition.Steps++;
                            wireMarks.Add(currentPosition.Clone());
                        }
                        break;
                    case 'U':
                        for (var i = 0; i < nSteps; i++)
                        {
                            currentPosition.Y++;
                            currentPosition.Steps++;
                            wireMarks.Add(currentPosition.Clone());
                        }
                        break;
                    case 'L':
                        for (var i = 0; i < nSteps; i++)
                        {
                            currentPosition.X--;
                            currentPosition.Steps++;
                            wireMarks.Add(currentPosition.Clone());
                        }
                        break;
                    case 'D':
                        for (var i = 0; i < nSteps; i++)
                        {
                            currentPosition.Y--;
                            currentPosition.Steps++;
                            wireMarks.Add(currentPosition.Clone());
                        }
                        break;
                }
            }

            return wireMarks;
        }

        private static List<GridMark> FetchIntersections(List<GridMark> firstWireMark, List<GridMark> secondWireMark)
        {
            return firstWireMark.Intersect(secondWireMark).ToList();
        }
    }

    internal class GridMark
    {
        internal GridMark(int x, int y, int steps)
        {
            X = x;
            Y = y;
            Steps = steps;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Steps { get; set; }

        public int ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }

        public GridMark Clone()
        {
            return (GridMark)MemberwiseClone();
        }

        public void Print()
        {
            Console.WriteLine($"My current position is {X} and {Y}");
        }

        public override bool Equals(object obj)
        {
            if (obj is GridMark other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 7 * X.GetHashCode() * Y.GetHashCode();
        }
    }
}
