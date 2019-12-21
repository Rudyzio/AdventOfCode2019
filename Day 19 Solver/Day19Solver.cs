using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_19_Solver
{
    public static class Day19Solver
    {
        public static int Part1Solution(long[] input, int maxX, int maxY)
        {
            int pulledPositions = 0;

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    if(IsPulled(input, x, y))
                    {
                        pulledPositions++;
                    }
                }
                Console.Write("\n");
            }

            return pulledPositions;
        }

        public static int Part2Solution(long[] input)
        {
            while (true)
            {
                int y = 10;
                int xStart = 0;
                while (true)
                {
                    y++;
                    while (!IsPulled(input, xStart, y))
                    {
                        xStart++;
                    };
                    int x = xStart;
                    while (IsPulled(input, x + 99, y))
                    {
                        if (IsPulled(input, x, y + 99))
                        {
                            return (x * 10000) + y;
                        }
                        x++;
                    }
                }
            }
        }

        private static bool IsPulled(long[] input, int x, int y)
        {
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);
            intCodeProgram.Input.Enqueue(x);
            intCodeProgram.Input.Enqueue(y);
            intCodeProgram.Run();

            DroneState droneState = (DroneState)intCodeProgram.Output.Dequeue();

            if (droneState == DroneState.Pulled)
            {
                Console.Write("#");
                return true;
            }
            else
            {
                Console.Write(".");
                return false;
            }
        }
    }

    public enum DroneState
    {
        Stationary = 0,
        Pulled = 1
    }

    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

}
