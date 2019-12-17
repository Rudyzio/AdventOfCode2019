using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_17_Solver
{
    public static class Day17Solver
    {
        public static int Part1Solution(long[] input)
        {
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);
            List<Position> positions = new List<Position>();

            intCodeProgram.Run();
            FillPositions(intCodeProgram.Output, positions);
            return CalculateIntersections(positions);
        }

        public static int Part2Solution(long[] input)
        {
            input[0] = 2;
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            long[] funcA = new long[] { (long)Ascii.Left, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.Comma,
                                        (long)Ascii.Left, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.Comma,
                                        (long)Ascii.Left, (long)Ascii.Comma, (long)Ascii.One, (long)Ascii.Zero, (long)Ascii.Comma,
                                        (long)Ascii.Right, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.NewLine };

            long[] funcB = new long[] { (long)Ascii.Right, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.Comma,
                                        (long)Ascii.Left, (long)Ascii.Comma, (long)Ascii.One, (long)Ascii.Zero, (long)Ascii.Comma,
                                        (long)Ascii.Right, (long)Ascii.Comma,(long)Ascii.One, (long)Ascii.Zero,(long)Ascii.NewLine };

            long[] funcC = new long[] { (long)Ascii.Right, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.Comma,
                                        (long)Ascii.Left, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.Comma,
                                        (long)Ascii.Left, (long)Ascii.Comma, (long)Ascii.Four, (long)Ascii.Comma,
                                        (long)Ascii.Right, (long)Ascii.Comma, (long)Ascii.Eight, (long)Ascii.Comma,
                                        (long)Ascii.Right, (long)Ascii.Comma, (long)Ascii.One, (long)Ascii.Zero, (long)Ascii.NewLine};

            long[] orderFunctions = new long[] { (long)Ascii.A, (long)Ascii.Comma,
                                                (long)Ascii.C, (long)Ascii.Comma,
                                                (long)Ascii.A, (long)Ascii.Comma,
                                                (long)Ascii.B, (long)Ascii.Comma,
                                                (long)Ascii.A, (long)Ascii.Comma,
                                                (long)Ascii.B, (long)Ascii.Comma,
                                                (long)Ascii.C, (long)Ascii.Comma,
                                                (long)Ascii.B, (long)Ascii.Comma,
                                                (long)Ascii.B, (long)Ascii.Comma,
                                                (long)Ascii.C, (long)Ascii.NewLine };

            //long[] askPrint = new long[] { (long)Ascii.y, (long)Ascii.NewLine }; // If we want to print result
            long[] askPrint = new long[] { (long)Ascii.n, (long)Ascii.NewLine };

            long[] inputToProgram = orderFunctions.Concat(funcA.Concat(funcB.Concat(funcC.Concat(askPrint)))).ToArray();
            foreach (var a in inputToProgram)
            {
                intCodeProgram.Input.Enqueue(a);
            }
            intCodeProgram.Run();
            //FillPositions(intCodeProgram.Output, new List<Position>()); // Uncomment if want to print result

            return (int)intCodeProgram.Output.Last();
        }

        private static void FillPositions(Queue<long> intCodeOutput, List<Position> positions)
        {
            int currentX = 0;
            int currentY = 0;
            while (intCodeOutput.Count > 1)
            {
                var output = (Content)intCodeOutput.Dequeue();
                switch (output)
                {
                    case Content.Scaffold:
                        positions.Add(new Position(currentX, currentY, output));
                        currentX++;
                        Console.Write("#");
                        break;
                    case Content.OpenSpace:
                        positions.Add(new Position(currentX, currentY, output));
                        currentX++;
                        Console.Write(".");
                        break;
                    case Content.NewLine:
                        currentX = 0;
                        currentY++;
                        Console.Write("\n");
                        break;
                    case Content.RobotUp:
                        positions.Add(new Position(currentX, currentY, output));
                        currentX++;
                        Console.Write("^");
                        break;
                    case Content.RobotLeft:
                        positions.Add(new Position(currentX, currentY, output));
                        currentX++;
                        Console.Write("<");
                        break;
                    case Content.RobotRight:
                        positions.Add(new Position(currentX, currentY, output));
                        currentX++;
                        Console.Write(">");
                        break;
                    case Content.RobotDown:
                        positions.Add(new Position(currentX, currentY, output));
                        currentX++;
                        Console.Write("v");
                        break;
                }
            }
        }

        private static int CalculateIntersections(List<Position> positions)
        {
            List<Position> scaffoldPositions = positions.Where(x => x.Content == Content.Scaffold || x.Content == Content.RobotUp).ToList();
            int toReturn = 0;
            foreach (var position in scaffoldPositions)
            {
                if (HasIntersection(scaffoldPositions, position))
                {
                    toReturn += (position.X * position.Y);
                }
            }
            return toReturn;
        }

        private static bool HasIntersection(List<Position> scaffoldPositions, Position position)
        {
            return scaffoldPositions.Any(pos => pos.X + 1 == position.X && pos.Y == position.Y) &&
                   scaffoldPositions.Any(pos => pos.X - 1 == position.X && pos.Y == position.Y) &&
                   scaffoldPositions.Any(pos => pos.X == position.X && pos.Y + 1 == position.Y) &&
                   scaffoldPositions.Any(pos => pos.X == position.X && pos.Y - 1 == position.Y);
        }
    }

    public class Position
    {
        public Position(int x, int y, Content content)
        {
            X = x;
            Y = y;
            Content = content;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Content Content { get; set; }
    }

    public enum Content
    {
        Scaffold = 35,
        OpenSpace = 46,

        RobotLeft = 60,
        RobotRight = 62,
        RobotUp = 94,
        RobotDown = 118,

        NewLine = 10
    }

    public enum Ascii
    {
        A = 65,
        B = 66,
        C = 67,
        NewLine = 10,
        Comma = 44,

        Left = 76,
        Right = 82,

        Zero = 48,
        One = 49,
        Two = 50,
        Three = 51,
        Four = 52,
        Five = 53,
        Six = 54,
        Seven = 55,
        Eight = 56,
        Nine = 57,

        y = 121,
        n = 110
    }
}
