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
            PositionsGraph<Position> positions = new PositionsGraph<Position>();
            return DFSGrid(input, positions);
        }

        public static int Part2Solution(long[] input)
        {
            PositionsGraph<Position> positions = new PositionsGraph<Position>();
            var levelsReached = new List<int>();
            DFSGrid(input, positions);
            int count = 0;
            positions.AdjacencyList.First(x => x.Key.HasOxygen).Key.Level = 0;
            while (positions.AdjacencyList.Any(x => !x.Key.HasOxygen))
            {
                var positionsWithOxygenNotVisited = positions.AdjacencyList.Where(x => x.Key.HasOxygen && !x.Key.Visited);
                foreach (var tuple in positionsWithOxygenNotVisited)
                {
                    tuple.Key.Visited = true;
                    foreach (var child in tuple.Value)
                    {
                        if (!positions.AdjacencyList.First(pos => pos.Key.X == child.X && pos.Key.Y == child.Y).Key.HasOxygen)
                        {
                            Console.WriteLine($"Marking {child.X}:{child.Y} to have oxygen and level {tuple.Key.Level}");
                            positions.AdjacencyList.First(pos => pos.Key.X == child.X && pos.Key.Y == child.Y).Key.HasOxygen = true;
                            positions.AdjacencyList.First(pos => pos.Key.X == child.X && pos.Key.Y == child.Y).Key.Level = tuple.Key.Level + 1;
                        }
                    }
                    count = tuple.Key.Level;
                }
            }
            return count;
        }

        private static int BFS(PositionsGraph<Position> graph, Position start, int count = 0)
        {
            var visited = new HashSet<Position>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return count;

            var queue = new Queue<Position>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
            }

            return count;
        }

        private static long DFSGrid(long[] input, PositionsGraph<Position> positions)
        {
            Droid droid = new Droid(new IntCodeProgram(input));

            Position currentPosition = new Position(0, 0, Answer.Allowed);
            positions.AddVertex(currentPosition);
            Stack<Command> currentCommands = new Stack<Command>();
            long currentDistance = 0;

            while (true)
            {
                bool foundDeadEnd = false;
                Console.WriteLine($"I'm at current position {currentPosition.X}:{currentPosition.Y}");
                Command command = CommandToTry(currentPosition);
                if (command == Command.NoCommand)
                {
                    Console.WriteLine($"Found dead end {currentPosition.X}:{currentPosition.Y}");
                    command = GetOpposite(currentCommands.Pop());
                    foundDeadEnd = true;
                    currentDistance--;
                }
                else
                {
                    currentCommands.Push(command);
                    currentDistance++;
                }

                var nextPosition = new Position(currentPosition.X, currentPosition.Y, Answer.Allowed);
                Console.WriteLine($"Going to go {command.ToString()}");
                switch (command)
                {
                    case Command.North:
                        nextPosition.Y++;
                        nextPosition.SouthTried = true;
                        currentPosition.NorthTried = true;
                        break;
                    case Command.South:
                        nextPosition.Y--;
                        nextPosition.NorthTried = true;
                        currentPosition.SouthTried = true;
                        break;
                    case Command.West:
                        nextPosition.X--;
                        nextPosition.EastTried = true;
                        currentPosition.WestTried = true;
                        break;
                    case Command.East:
                        nextPosition.X++;
                        nextPosition.WestTried = true;
                        currentPosition.EastTried = true;
                        break;
                    case Command.NoCommand:
                        throw new Exception("No Command passed through");
                }

                Answer answer = droid.Move(command);
                switch (answer)
                {
                    case Answer.Wall:
                        if (!foundDeadEnd)
                            currentCommands.Pop();
                        Console.WriteLine("Hit a wall");
                        break;
                    case Answer.Allowed:
                        if (!positions.ContainsVertex(nextPosition))
                        {
                            positions.AddVertex(nextPosition);
                            positions.AddEdge(Tuple.Create(currentPosition, nextPosition));
                        }
                        nextPosition = positions.GetVertex(nextPosition).Key;
                        Console.WriteLine($"Moved from {currentPosition.X}:{currentPosition.Y} to {nextPosition.X}:{nextPosition.Y}");
                        nextPosition.Distance = currentCommands.Count;
                        currentPosition = nextPosition;
                        break;
                    case Answer.Success:
                        if (!positions.ContainsVertex(nextPosition))
                        {
                            positions.AddVertex(nextPosition);
                            positions.AddEdge(Tuple.Create(currentPosition, nextPosition));
                        }
                        nextPosition = positions.GetVertex(nextPosition).Key;
                        Console.WriteLine($"Moved from {currentPosition.X}:{currentPosition.Y} to {nextPosition.X}:{nextPosition.Y} OXYGEN");
                        nextPosition.HasOxygen = true;
                        nextPosition.Distance = currentCommands.Count;
                        currentPosition = nextPosition;
                        break;
                        //return 100;
                }

                Console.WriteLine($"I still have {currentCommands.Count} commands on the queue");
                if (currentCommands.Count == 0 && positions.AdjacencyList.All(x => x.Key.EastTried && x.Key.NorthTried && x.Key.SouthTried && x.Key.WestTried))
                {
                    return positions.AdjacencyList.First(x => x.Key.HasOxygen).Key.Distance;
                }
            }
        }

        private static Command CommandToTry(Position currentPosition)
        {
            if (!currentPosition.NorthTried)
            {
                currentPosition.NorthTried = true;
                return Command.North;
            }

            if (!currentPosition.SouthTried)
            {
                currentPosition.SouthTried = true;
                return Command.South;
            }

            if (!currentPosition.EastTried)
            {
                currentPosition.EastTried = true;
                return Command.East;
            }

            if (!currentPosition.WestTried)
            {
                currentPosition.WestTried = true;
                return Command.West;
            }

            return Command.NoCommand;
        }

        private static Command GetOpposite(Command command)
        {
            switch (command)
            {
                case Command.North:
                    return Command.South;
                case Command.South:
                    return Command.North;
                case Command.West:
                    return Command.East;
                case Command.East:
                    return Command.West;
            }
            throw new Exception("Something went wrong");
        }
    }

    public class Graph<T>
    {
        public Graph() { }
        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

        public void AddVertex(T vertex)
        {
            AdjacencyList[vertex] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }

    public class PositionsGraph<T> : Graph<T> where T : Position
    {
        public KeyValuePair<T, HashSet<T>> GetVertex(T vertex)
        {
            return AdjacencyList.FirstOrDefault(x => x.Key.X == vertex.X && x.Key.Y == vertex.Y);
        }

        public bool ContainsVertex(T vertex)
        {
            return AdjacencyList.Any(x => x.Key.X == vertex.X && x.Key.Y == vertex.Y);
        }

        public void RemoveVertex(T vertex)
        {
            AdjacencyList.Remove(GetVertex(vertex).Key);
        }
    }
}
