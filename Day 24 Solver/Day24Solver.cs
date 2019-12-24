using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_24_Solver
{
    public static class Day24Solver
    {
        public static long Part1Solution(string[] input)
        {
            var currentPositions = ReadInput(input);
            var newState = new State(currentPositions.Select(pos => pos.Clone()).ToList());
            var historyStates = new List<State>
            {
                newState
            };

            while (true)
            {
                currentPositions.Print();
                Step(currentPositions);
                newState = new State(currentPositions.Select(pos => pos.Clone()).ToList());

                if (historyStates.Contains(newState))
                {
                    break;
                }

                historyStates.Add(newState);
            }

            return newState.GetBiodiversity();
        }

        public static long Part2Solution(string[] input, int minutesLimit)
        {
            var currentPositions = ReadInput(input);
            Dictionary<int, State> states = new Dictionary<int, State>
            {
                { 0, new State(currentPositions, 0) }
            };

            int minutes = 0;
            while (minutes < minutesLimit)
            {
                int newMaxLevel = states.Keys.Max() + 1;
                int newMinLevel = states.Keys.Min() - 1;

                states.Add(newMaxLevel, new State(newMaxLevel));
                states.Add(newMinLevel, new State(newMinLevel));

                StepLevels(states);

                minutes++;
            }

            long toReturn = 0;
            foreach (var pair in states)
            {
                toReturn += pair.Value.GetBugs();
            }

            return toReturn;
        }

        private static List<Position> ReadInput(string[] input)
        {
            var toReturn = new List<Position>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    toReturn.Add(new Position(x, y, input[y][x].ToString().Equals(".") ? Content.Empty : Content.Bug));
                }
            }

            return toReturn;
        }

        private static List<Position> AdjacentPositions(List<Position> positions, Position position)
        {
            return positions.Where(pos => pos.X + 1 == position.X && pos.Y == position.Y ||
                                          pos.X - 1 == position.X && pos.Y == position.Y ||
                                          pos.X == position.X && pos.Y + 1 == position.Y ||
                                          pos.X == position.X && pos.Y - 1 == position.Y).ToList();
        }

        private static List<Position> AdjacentPositionLevels(Dictionary<int, State> states, Position position, int level)
        {
            var adjacents = AdjacentPositions(states[level].Positions, position);

            // If it is adjacent to the center
            if (adjacents.Any(x => x.IsCenter()))
            {
                //var lowerLevel = states[level + 1];
                states.TryGetValue(level + 1, out var lowerLevel);
                if (lowerLevel == null)
                    return adjacents;

                // Top
                if (position.Y == 1)
                    adjacents.AddRange(lowerLevel.GetTopPositions());

                // Left
                if (position.X == 1)
                    adjacents.AddRange(lowerLevel.GetLeftPositions());

                // Right
                if (position.X == 3)
                    adjacents.AddRange(lowerLevel.GetRightPositions());

                // Bottom
                if (position.Y == 3)
                    adjacents.AddRange(lowerLevel.GetBottomPositions());

                return adjacents.Where(pos => !pos.IsCenter()).ToList();
            }

            //var higherLevel = states[level - 1];
            states.TryGetValue(level - 1, out var higherLevel);
            if (higherLevel == null)
                return adjacents;

            // On the top edge
            if (position.Y == 0)
                adjacents.Add(higherLevel.GetTopCenter());

            // On left edge
            if (position.X == 0)
                adjacents.Add(higherLevel.GetLeftCenter());

            // On right edge
            if (position.X == 4)
                adjacents.Add(higherLevel.GetRightCenter());

            // On bottom edge
            if (position.Y == 4)
                adjacents.Add(higherLevel.GetBottomCenter());

            return adjacents;
        }

        private static void Step(List<Position> positions)
        {
            // Setup
            foreach (var position in positions)
            {
                var adjacentPositions = AdjacentPositions(positions, position);
                if (position.IsEmptySpace() && (adjacentPositions.Count(pos => !pos.IsEmptySpace()) == 1 ||
                                                adjacentPositions.Count(pos => !pos.IsEmptySpace()) == 2))
                {
                    position.Change = true;
                }

                if (!position.IsEmptySpace() && adjacentPositions.Count(pos => !pos.IsEmptySpace()) != 1)
                {
                    position.Change = true;
                }
            }

            // Step
            foreach (var position in positions)
            {
                if (position.Change)
                {
                    if (position.IsEmptySpace())
                    {
                        position.Content = Content.Bug;
                    }
                    else
                    {
                        position.Content = Content.Empty;
                    }
                }
                position.Change = false;
            }
        }

        private static void StepLevels(Dictionary<int, State> states)
        {
            // Setup
            foreach (var state in states)
            {
                foreach (var position in state.Value.Positions)
                {
                    var adjacentPositions = AdjacentPositionLevels(states, position, state.Key);
                    if (position.IsEmptySpace() && (adjacentPositions.Count(pos => !pos.IsEmptySpace()) == 1 ||
                                                    adjacentPositions.Count(pos => !pos.IsEmptySpace()) == 2))
                    {
                        position.Change = true;
                    }

                    if (!position.IsEmptySpace() && adjacentPositions.Count(pos => !pos.IsEmptySpace()) != 1)
                    {
                        position.Change = true;
                    }
                }
            }

            // Step
            foreach (var state in states)
            {
                foreach (var position in state.Value.Positions)
                {
                    if (position.Change)
                    {
                        if (position.IsEmptySpace())
                        {
                            position.Content = Content.Bug;
                        }
                        else
                        {
                            position.Content = Content.Empty;
                        }
                    }
                    position.Change = false;
                }
            }
        }

        private static void Print(this List<Position> positions)
        {
            var stepsToNewLine = 5;
            var current = 1;
            for (var i = 0; i < positions.Count; i++)
            {
                Console.Write(positions[i].IsEmptySpace() ? "." : "#");
                if (current == stepsToNewLine)
                {
                    current = 0;
                    Console.Write("\n");
                }
                current++;
            }
            Console.Write("\n");
        }
    }

    public class Position
    {
        public Position(int x, int y, Content content)
        {
            X = x;
            Y = y;
            Content = content;
            Change = false;
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Content = Content.Empty;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Content Content { get; set; }
        public bool Change { get; set; }

        public bool IsEmptySpace()
        {
            return Content == Content.Empty;
        }

        public bool IsCenter()
        {
            return X == 2 && Y == 2;
        }

        public Position Clone()
        {
            return new Position(X, Y, Content);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position item))
            {
                return false;
            }

            return X == item.X && Y == item.Y && Content == item.Content;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public class State
    {
        public State(List<Position> positions)
        {
            Positions = positions.ToList();
        }

        public State(int level)
        {
            Level = level;
            Positions = new List<Position>();

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    Positions.Add(new Position(x, y));
                }
            }
        }

        public State(List<Position> positions, int level)
        {
            Positions = positions.ToList();
            Level = level;
        }

        public List<Position> Positions { get; set; }

        public int Level { get; set; }

        public List<Position> GetTopPositions()
        {
            return Positions.Where(pos => pos.Y == 0).ToList();
        }

        public List<Position> GetLeftPositions()
        {
            return Positions.Where(pos => pos.X == 0).ToList();
        }

        public List<Position> GetRightPositions()
        {
            return Positions.Where(pos => pos.X == 4).ToList();
        }

        public List<Position> GetBottomPositions()
        {
            return Positions.Where(pos => pos.Y == 4).ToList();
        }

        public Position GetTopCenter()
        {
            return Positions.Single(pos => pos.X == 2 && pos.Y == 1);
        }

        public Position GetLeftCenter()
        {
            return Positions.Single(pos => pos.X == 1 && pos.Y == 2);
        }

        public Position GetRightCenter()
        {
            return Positions.Single(pos => pos.X == 3 && pos.Y == 2);
        }

        public Position GetBottomCenter()
        {
            return Positions.Single(pos => pos.X == 2 && pos.Y == 3);
        }

        public long GetBiodiversity()
        {
            long toReturn = 0;
            for (var i = 0; i < Positions.Count; i++)
            {
                if (!Positions[i].IsEmptySpace())
                {
                    toReturn += (long)Math.Pow(2, i);
                }
            }

            return toReturn;
        }

        public long GetBugs()
        {
            long toReturn = 0;
            for (var i = 0; i < Positions.Count; i++)
            {
                if (!Positions[i].IsEmptySpace() && !Positions[i].IsCenter())
                {
                    toReturn++;
                }
            }

            return toReturn;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is State item))
            {
                return false;
            }

            return !Positions.Except(item.Positions).Any() && Level == item.Level;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Positions);
        }
    }

    public enum Content
    {
        Empty = 0,
        Bug = 1
    }
}
