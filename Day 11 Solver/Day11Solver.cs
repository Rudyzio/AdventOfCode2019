using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_11_Solver
{
    public static class Day11Solver
    {
        public static int Part1Solution(long[] input)
        {
            int paintedAtLeastOnce = 0;
            List<GridMark> gridMarks = new List<GridMark>();
            var currentPosition = new GridMark(0, 0, Colors.Black);
            var currentDirection = Direction.Up;

            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            while (true)
            {
                currentPosition.Color = GetPositionColor(currentPosition, gridMarks);
                if (currentPosition.Color == Colors.Black)
                {
                    intCodeProgram.Input.Enqueue(0);
                }
                else
                {
                    intCodeProgram.Input.Enqueue(1);
                }

                var type = intCodeProgram.Run();

                if (type == Halt.Terminated)
                {
                    return paintedAtLeastOnce;
                }

                currentPosition.Color = (Colors)intCodeProgram.Output.Dequeue();
                long directionToTurn = intCodeProgram.Output.Dequeue();

                GridMark existingMark = gridMarks.SingleOrDefault(pos => pos.X == currentPosition.X && pos.Y == currentPosition.Y);
                if (existingMark != null)
                {
                    gridMarks.Remove(existingMark);
                }
                else
                {
                    paintedAtLeastOnce++;
                }
                gridMarks.Add(new GridMark(currentPosition.X, currentPosition.Y, currentPosition.Color));

                currentDirection = SetDirection(currentDirection, directionToTurn);
                currentPosition = SetPosition(currentPosition, currentDirection);
            }
        }

        public static int Part2Solution(long[] input)
        {
            int paintedAtLeastOnce = 0;
            List<GridMark> gridMarks = new List<GridMark>();
            var currentPosition = new GridMark(0, 0, Colors.Black);
            var currentDirection = Direction.Up;

            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            while (true)
            {
                if (gridMarks.Count == 0)
                {
                    currentPosition.Color = Colors.White;
                }
                else
                {
                    currentPosition.Color = GetPositionColor(currentPosition, gridMarks);
                }

                if (currentPosition.Color == Colors.Black)
                {
                    intCodeProgram.Input.Enqueue(0);
                }
                else
                {
                    intCodeProgram.Input.Enqueue(1);
                }

                var type = intCodeProgram.Run();

                if (type == Halt.Terminated)
                {
                    Print(gridMarks);
                    return paintedAtLeastOnce;
                }

                currentPosition.Color = (Colors)intCodeProgram.Output.Dequeue();
                long directionToTurn = intCodeProgram.Output.Dequeue();

                GridMark existingMark = gridMarks.SingleOrDefault(pos => pos.X == currentPosition.X && pos.Y == currentPosition.Y);
                if (existingMark != null)
                {
                    gridMarks.Remove(existingMark);
                }
                else
                {
                    paintedAtLeastOnce++;
                }
                gridMarks.Add(new GridMark(currentPosition.X, currentPosition.Y, currentPosition.Color));

                currentDirection = SetDirection(currentDirection, directionToTurn);
                currentPosition = SetPosition(currentPosition, currentDirection);
            }
        }

        private static Colors GetPositionColor(GridMark mark, List<GridMark> gridMarks)
        {
            var position = gridMarks.SingleOrDefault(pos => pos.X == mark.X && pos.Y == mark.Y);
            if (position != null)
            {
                return position.Color;
            }
            else
            {
                return Colors.Black;
            }
        }

        private static Direction SetDirection(Direction currentDirection, long directionToTurn)
        {
            if (directionToTurn == 0)
            {
                switch (currentDirection)
                {
                    case Direction.Up:
                        return Direction.Left;
                    case Direction.Right:
                        return Direction.Up;
                    case Direction.Down:
                        return Direction.Right;
                    case Direction.Left:
                        return Direction.Down;
                }
            }
            else
            {
                switch (currentDirection)
                {
                    case Direction.Up:
                        return Direction.Right;
                    case Direction.Right:
                        return Direction.Down;
                    case Direction.Down:
                        return Direction.Left;
                    case Direction.Left:
                        return Direction.Up;
                }
            }
            return currentDirection; // Dummy
        }

        private static GridMark SetPosition(GridMark position, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    position.Y++;
                    break;
                case Direction.Right:
                    position.X++;
                    break;
                case Direction.Down:
                    position.Y--;
                    break;
                case Direction.Left:
                    position.X--;
                    break;
            }
            return position;
        }

        private static void Print(List<GridMark> gridMarks)
        {
            int minY = Math.Abs(gridMarks.Min(mark => mark.Y));
            int minX = Math.Abs(gridMarks.Min(mark => mark.X));

            int maxY = gridMarks.Max(mark => mark.Y);
            int maxX = gridMarks.Max(mark => mark.X);

            int yLength = minY + maxY;
            int xLength = minX + maxX;

            gridMarks = gridMarks.Select(item => new GridMark(item.X + minX, item.Y + minY, item.Color)).ToList();

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
            foreach (var mark in gridMarks)
            {
                test++;
                matrix[mark.Y][mark.X] = mark.Color == Colors.Black ? "." : "#";
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

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public enum Colors
    {
        Black = 0,
        White = 1
    }

    public class GridMark
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Colors Color { get; set; }

        public GridMark(int X, int Y, Colors color)
        {
            this.X = X;
            this.Y = Y;
            Color = color;
        }
    }
}
