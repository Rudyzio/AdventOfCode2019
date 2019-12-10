using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_10_Solver
{
    public static class Day10Solver
    {
        private const string ASTEROID = "#";

        public static int Part1Solution(string[][] input)
        {
            var originalAsteroidPositions = GetAsteroidPositions(input);
            int highestScore = 0;
            int positionX = -1;
            int positionY = -1;

            foreach (var asteroid in originalAsteroidPositions)
            {
                var asteroidPositions = originalAsteroidPositions.ToList();
                int asteroids = CountAsteroidsSight((asteroid.Key, asteroid.Value), asteroidPositions, input.Length, input[0].Length);

                positionX = highestScore > asteroids ? positionX : asteroid.Key;
                positionY = highestScore > asteroids ? positionY : asteroid.Value;
                highestScore = highestScore > asteroids ? highestScore : asteroids;
            }
            return highestScore;
        }

        public static int Part2Solution(string[][] input)
        {
            var originalAsteroidPositions = GetAsteroidPositions(input);
            int highestScore = 0;
            int positionX = -1;
            int positionY = -1;
            int yLength = input.Length;
            int xLength = input[0].Length;

            // Same as part 1
            foreach (var asteroid in originalAsteroidPositions)
            {
                var asteroidPositions = originalAsteroidPositions.ToList();
                int asteroids = CountAsteroidsSight((asteroid.Key, asteroid.Value), asteroidPositions, input.Length, input[0].Length);

                positionX = highestScore > asteroids ? positionX : asteroid.Key;
                positionY = highestScore > asteroids ? positionY : asteroid.Value;
                highestScore = highestScore > asteroids ? highestScore : asteroids;
            }

            // Part 2 for real
            int counter = 0;
            var limits = GetAsteroidMapLimits(input, positionX);
            (int x, int y) referenceAsteroid = (positionX, positionY);
            originalAsteroidPositions.Remove(new KeyValuePair<int, int>(referenceAsteroid.x, referenceAsteroid.y));

            List<(int x, int y, double angle)> asteroidAngles = new List<(int x, int y, double angle)>();
            foreach (var asteroid in originalAsteroidPositions)
            {
                (int x, int y) vector = FindDirectionVector((asteroid.Key, asteroid.Value), referenceAsteroid);
                asteroidAngles.Add((asteroid.Key, asteroid.Value, GetAngle(vector.x, vector.y)));
            }
            asteroidAngles = asteroidAngles.OrderBy(x => x.angle).ToList();

            int index = 0;
            double lastAngle = -1;
            while (asteroidAngles.Count > 0)
            {
                double currentAngle = asteroidAngles[index].angle;
                if (currentAngle == lastAngle)
                {
                    index++;
                    if (index >= asteroidAngles.Count)
                    {
                        index = 0;
                    }
                    continue;
                }

                var allWithAngle = asteroidAngles.Where(ast => ast.angle == currentAngle).ToList();
                (int x, int y, double angle) toRemove;
                if (allWithAngle.Count == 1)
                {
                    toRemove = allWithAngle.Single();
                }
                else
                {
                    toRemove = GetClosestAsteroid(allWithAngle, referenceAsteroid);
                }

                counter++;
                if (counter == 200)
                {
                    return toRemove.x * 100 + toRemove.y;
                }
                asteroidAngles.Remove(toRemove);

                lastAngle = currentAngle;

                if (index >= asteroidAngles.Count)
                {
                    index = 0;
                }
            }
            return 0; // dummy return
        }

        private static int CountAsteroidsSight((int x, int y) referenceAsteroid, List<KeyValuePair<int, int>> asteroids, int xLength, int yLength)
        {
            asteroids.Remove(new KeyValuePair<int, int>(referenceAsteroid.x, referenceAsteroid.y));
            var asteroidsToPurge = asteroids.ToList();
            foreach (var asteroid in asteroids)
            {
                (int x, int y) targetAsteroid = (asteroid.Key, asteroid.Value);
                (int x, int y) vector = FindDirectionVector(referenceAsteroid, targetAsteroid);

                (int x, int y) currentPosition = (targetAsteroid.x + vector.x, targetAsteroid.y + vector.y);
                while (currentPosition.x < xLength && currentPosition.x >= 0 && currentPosition.y < yLength && currentPosition.y >= 0)
                {
                    asteroidsToPurge.Remove(new KeyValuePair<int, int>(currentPosition.x, currentPosition.y));

                    currentPosition = (currentPosition.x + vector.x, currentPosition.y + vector.y);
                }
            }

            return asteroidsToPurge.Count;
        }

        private static (int x, int y) FindDirectionVector((int x, int y) referenceAsteroid, (int x, int y) targetAsteroid)
        {
            int xCoord = targetAsteroid.x - referenceAsteroid.x;
            int yCoord = targetAsteroid.y - referenceAsteroid.y;

            // Error was here --> not dividing for greatest common divisor --> to get every asteroid in the vector
            int gcd = FindGreatestCommonDivisor(xCoord, yCoord);

            if (gcd > 0)
            {
                xCoord /= gcd;
                yCoord /= gcd;
            }


            return (xCoord, yCoord);
        }

        private static int FindGreatestCommonDivisor(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        private static List<KeyValuePair<int, int>> GetAsteroidPositions(string[][] input)
        {
            List<KeyValuePair<int, int>> asteroids = new List<KeyValuePair<int, int>>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x].Equals(ASTEROID))
                    {
                        asteroids.Add(new KeyValuePair<int, int>(x, y));
                    }
                }
            }

            return asteroids;
        }

        private static List<KeyValuePair<int, int>> GetAsteroidMapLimits(string[][] input, int xPosition)
        {
            List<KeyValuePair<int, int>> limits = new List<KeyValuePair<int, int>>();
            var xLength = input[0].Length;
            var yLength = input.Length;

            for (var i = xPosition; i < xLength; i++)
            {
                limits.Add(new KeyValuePair<int, int>(i, 0));
            }

            for (var i = 0; i < yLength; i++)
            {
                limits.Add(new KeyValuePair<int, int>(xLength - 1, i));
            }

            for (var i = xLength - 1; i >= 0; i--)
            {
                limits.Add(new KeyValuePair<int, int>(i, yLength - 1));
            }

            for (var i = yLength - 1; i >= 0; i--)
            {
                limits.Add(new KeyValuePair<int, int>(0, i));
            }

            for (var i = 0; i < xPosition; i++)
            {
                limits.Add(new KeyValuePair<int, int>(i, 0));
            }

            return limits.Distinct().ToList();
        }

        private static double GetAngle(int x, int y)
        {
            // Trigonometric circle is inverted like this, so lets put it good
            y = -y;
            x = -x;

            double radians = Math.Atan2(y, x);
            double degrees = RadiansToDegrees(radians);

            if (degrees < 0)
            {
                degrees += 360;
            }

            /*
             * Right now if we look at the trigonometric circle is how we learn in school:
             *      Center point to the right ----> 0 degrees
             *      Center point to up -----------> 90 degrees
             *      Center point to left ---------> 180 degrees
             *      Center point to down ---------> 270 degrees
             *      
             * We need to put our up as 0 --> or rotate the trigonometric circle
             * 
             * Sum 90 degrees to everything
             * Everything that goes beyond 360 (included), subract 360 et voila, we have our trigonometric circle how we want
             * 
             *      Center point to the right ----> 90 degrees
             *      Center point to up -----------> 0 degrees
             *      Center point to left ---------> 270 degrees
             *      Center point to down ---------> 180 degrees
             * 
             */

            degrees += 90;

            if (degrees >= 360)
            {
                degrees -= 360;
            }

            return degrees;
        }

        private static double RadiansToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }

        private static (int x, int y, double angle) GetClosestAsteroid(List<(int x, int y, double angle)> asteroids, (int x, int y) referenceAsteroid)
        {
            (int x, int y, double angle) toReturn = (0, 0, 0);
            int closestDistance = -1;
            foreach (var ast in asteroids)
            {
                var currentDistance = Math.Abs(referenceAsteroid.x - ast.x) + Math.Abs(referenceAsteroid.y - ast.y);
                if (closestDistance == -1 || currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    toReturn = ast;
                }
            }

            return toReturn;
        }
    }
}
