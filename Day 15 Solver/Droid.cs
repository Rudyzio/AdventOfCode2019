using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_15_Solver
{
    public class Droid
    {
        private IntCodeProgram intCodeProgram;

        public long Distance { get; private set; }

        public Position Position { get; set; }

        public bool Success { get; set; }

        public Droid(Position position, IntCodeProgram program, bool success = false)
        {
            intCodeProgram = program;
            Position = position;
            Success = success;
        }

        public Droid MoveBFS(Command command, List<Position> exploredPositions)
        {
            var nextPosition = new Position(Position.X, Position.Y, Answer.Allowed);
            switch (command)
            {
                case Command.North:
                    nextPosition.Y++;
                    break;
                case Command.East:
                    nextPosition.X++;
                    break;
                case Command.South:
                    nextPosition.Y--;
                    break;
                case Command.West:
                    nextPosition.X--;
                    break;
            }

            if (exploredPositions.Any(pos => pos.X == nextPosition.X && pos.Y == nextPosition.Y))
            {
                return null;
            }
            else
            {
                exploredPositions.Add(nextPosition);
            }

            intCodeProgram.Input.Enqueue((long)command);
            intCodeProgram.Run();
            var answer = intCodeProgram.Output.Dequeue();

            exploredPositions.Single(pos => pos.X == nextPosition.X && pos.Y == nextPosition.Y).Content = (Answer)answer;

            switch ((Answer)answer)
            {
                case Answer.Wall:
                    return null;
                case Answer.Allowed:
                    return new Droid(nextPosition, intCodeProgram.Clone());
                case Answer.Success:
                    return new Droid(nextPosition, intCodeProgram.Clone(), true);
            }

            throw new Exception("This is not supposed to happen");
        }
    }

    public class Position
    {
        public Position(int x, int y, Answer content)
        {
            X = x;
            Y = y;
            Content = content;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Answer Content { get; set; }
    }

    public enum Command
    {
        North = 1,
        South = 2,
        West = 3,
        East = 4
    }

    public enum Answer
    {
        Wall = 0,
        Allowed = 1,
        Success = 2
    }
}
