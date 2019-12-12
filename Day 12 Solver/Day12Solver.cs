using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_12_Solver
{
    public static class Day12Solver
    {
        public static int Part1Solution(int[][] input, int steps)
        {
            int step = 0;
            List<Moon> moons = GetMoons(input);
            while (step < steps)
            {
                Step(moons);
                step++;
            }

            return moons.Sum(x => x.GetTotalEnergy());
        }

        public static long Part2Solution(int[][] input)
        {
            int stepsToRepeatX = FindStepRepitionX(input);
            int stepsToRepeatY = FindStepRepitionY(input);
            int stepsToRepeatZ = FindStepRepitionZ(input);

            return LowestCommonMultiple3Numbers(stepsToRepeatX, stepsToRepeatY, stepsToRepeatZ);
        }

        private static List<Moon> GetMoons(int[][] input)
        {
            List<Moon> toReturn = new List<Moon>();
            for (int i = 0; i < input.Length; i++)
            {
                toReturn.Add(new Moon(new Coordinates(input[i][0], input[i][1], input[i][2]), new Coordinates(0, 0, 0)));
            }
            return toReturn;
        }

        private static void Step(List<Moon> moons)
        {
            for (int i = 0; i < moons.Count; i++)
            {
                // Only apply to moons that are ahead on the list
                for (int j = i + 1; j < moons.Count; j++)
                {
                    Moon moon1 = moons[i];
                    Moon moon2 = moons[j];

                    ApplyGravityX(moon1, moon2);
                    ApplyGravityY(moon1, moon2);
                    ApplyGravityZ(moon1, moon2);
                }
            }

            ApplyVelocity(moons);
        }

        private static void ApplyGravityX(Moon moon1, Moon moon2)
        {
            if (moon1.Position.X > moon2.Position.X)
            {
                moon1.Velocity.X--;
                moon2.Velocity.X++;
                return;
            }

            if (moon1.Position.X < moon2.Position.X)
            {
                moon1.Velocity.X++;
                moon2.Velocity.X--;
                return;
            }
        }

        private static void ApplyGravityY(Moon moon1, Moon moon2)
        {
            if (moon1.Position.Y > moon2.Position.Y)
            {
                moon1.Velocity.Y--;
                moon2.Velocity.Y++;
                return;
            }

            if (moon1.Position.Y < moon2.Position.Y)
            {
                moon1.Velocity.Y++;
                moon2.Velocity.Y--;
                return;
            }
        }

        private static void ApplyGravityZ(Moon moon1, Moon moon2)
        {
            if (moon1.Position.Z > moon2.Position.Z)
            {
                moon1.Velocity.Z--;
                moon2.Velocity.Z++;
                return;
            }

            if (moon1.Position.Z < moon2.Position.Z)
            {
                moon1.Velocity.Z++;
                moon2.Velocity.Z--;
                return;
            }
        }

        private static void ApplyVelocity(List<Moon> moons)
        {
            foreach (var moon in moons)
            {
                moon.Position.X += moon.Velocity.X;
                moon.Position.Y += moon.Velocity.Y;
                moon.Position.Z += moon.Velocity.Z;
            }
        }

        private static int FindStepRepitionX(int[][] input)
        {
            List<Moon> moons = GetMoons(input);
            List<int> initialXcoords = GetMoons(input).Select(moon => moon.Position.X).ToList();
            List<int> currentXCoords = new List<int>();
            int steps = 1;
            do
            {
                Step(moons);
                steps++;
                currentXCoords = moons.Select(moon => moon.Position.X).ToList();
            } while (!currentXCoords.SequenceEqual(initialXcoords));
            return steps;
        }

        private static int FindStepRepitionY(int[][] input)
        {
            List<Moon> moons = GetMoons(input);
            List<int> initialYcoords = GetMoons(input).Select(moon => moon.Position.Y).ToList();
            List<int> currentYCoords = new List<int>();
            int steps = 1;
            do
            {
                Step(moons);
                steps++;
                currentYCoords = moons.Select(moon => moon.Position.Y).ToList();
            } while (!currentYCoords.SequenceEqual(initialYcoords));
            return steps;
        }

        private static int FindStepRepitionZ(int[][] input)
        {
            List<Moon> moons = GetMoons(input);
            List<int> initialZcoords = GetMoons(input).Select(moon => moon.Position.Z).ToList();
            List<int> currentZCoords = new List<int>();
            int steps = 1;
            do
            {
                Step(moons);
                steps++;
                currentZCoords = moons.Select(moon => moon.Position.Z).ToList();
            } while (!currentZCoords.SequenceEqual(initialZcoords));
            return steps;
        }

        private static long LowestCommonMultiple3Numbers(long x, long y, long z)
        {
            return LowestCommonMultiple2Numbers(x, LowestCommonMultiple2Numbers(y, z));
        }

        private static long LowestCommonMultiple2Numbers(long x, long y)
        {
            return Math.Abs(x * y) / GreatestCommonDivisor2Numbers(x, y);
        }

        private static long GreatestCommonDivisor2Numbers(long x, long y)
        {
            if (y == 0)
            {
                return x;
            }
            else
            {
                return GreatestCommonDivisor2Numbers(y, x % y);
            }
        }

    }

    public class Moon
    {
        public Moon(Coordinates position, Coordinates velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public Coordinates Position { get; set; }

        public Coordinates Velocity { get; set; }

        public int GetTotalEnergy()
        {
            return GetPotentialEnergy() * GetKineticEnergy();
        }

        private int GetPotentialEnergy()
        {
            return Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
        }

        private int GetKineticEnergy()
        {
            return Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
        }
    }

    public class Coordinates
    {
        public Coordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
    }
}
