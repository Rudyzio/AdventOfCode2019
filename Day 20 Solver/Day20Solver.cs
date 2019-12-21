using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_20_Solver
{
    public static class Day20Solver
    {
        public static int Part1Solution(string[] input)
        {
            var graph = ReadInput(input);
            graph.ConnectPortalEdges();

            var initialPosition = graph.GetInitialVertex();
            var finalPosition = graph.GetFinalVertex();

            var path = graph.BFSShortestPath(initialPosition, finalPosition);
            for (var i = 0; i < path.Count; i++)
            {
                Console.WriteLine($"{path[i].X} {path[i].Y} {path[i].Content}");
            }

            return path.Count - 3; // Subtract AA, ZZ and first position
        }

        public static int Part2Solution(string[] input)
        {
            var graph = ReadInput(input);
            int maxLevels = graph.GetMaxLevels();

            //graph.ConnectPortalEdges();

            var initialPosition = graph.GetInitialVertex();
            var finalPosition = graph.GetFinalVertex();

            var amount = graph.BFSShortestPathDistanceLevels(initialPosition, finalPosition, maxLevels);
            return amount - 2; // Subtract AA, ZZ -- this BFS takes into account the starting position as distance 0
        }

        private static PositionsGraph ReadInput(string[] input)
        {
            var positions = new PositionsGraph();
            List<Position> allPositions = new List<Position>();
            int x = 0;
            int y = 0;
            foreach (var line in input)
            {
                foreach (var character in line)
                {
                    switch (character)
                    {
                        case '#':
                        case ' ':
                            break;
                        case '.':
                            allPositions.Add(new Position(x, y, "."));
                            break;
                        default:
                            allPositions.Add(new Position(x, y, character.ToString()));
                            break;

                    }
                    x++;
                }
                x = 0;
                y++;
            }

            foreach (var position in allPositions)
            {
                var adjacentPositions = GetAdjacentPositions(allPositions, position);
                if (adjacentPositions.Any(x => x.Content.Equals(".")))
                {
                    positions.AddVertex(position);
                    foreach (var adjacentPosition in adjacentPositions)
                    {
                        if (adjacentPosition.Content.Length == 1 && char.IsUpper(char.Parse(adjacentPosition.Content)) && !GetAdjacentPositions(allPositions, adjacentPosition).Any(x => x.Content.Equals(".")))
                        {
                            positions.AdjacencyList.Single(x => x.Key.Content.Equals(position.Content)).Key.Content += adjacentPosition.Content;
                            if (positions.AdjacencyList.Count(x => x.Key.Content.Equals(position.Content)) == 1)
                                positions.AdjacencyList.Single(x => x.Key.Content.Equals(position.Content)).Key.Content = String.Concat(positions.AdjacencyList.Single(x => x.Key.Content.Equals(position.Content)).Key.Content.OrderBy(c => c));
                        }
                        else
                        {
                            positions.AddEdge(new Tuple<Position, Position>(position, adjacentPosition));
                        }
                    }
                }
            }

            //foreach (var position in allPositions)
            //{
            //    positions.AddVertex(position);
            //    var adjacentPositions = GetAdjacentPositions(allPositions, position);
            //    foreach (var adjacentPosition in adjacentPositions)
            //    {
            //        positions.AddEdge(new Tuple<Position, Position>(position, adjacentPosition));
            //    }
            //}

            return positions;
        }

        private static List<Position> GetAdjacentPositions(List<Position> positions, Position position)
        {
            return positions.Where(pos => pos.X + 1 == position.X && pos.Y == position.Y ||
                                          pos.X - 1 == position.X && pos.Y == position.Y ||
                                          pos.X == position.X && pos.Y + 1 == position.Y ||
                                          pos.X == position.X && pos.Y - 1 == position.Y).ToList();
        }

        private static void ConnectPortalEdges(this PositionsGraph graph)
        {
            while (true)
            {
                var portalPositions = graph.AdjacencyList.Where(x => x.Key.Content.All(char.IsUpper) && !x.Key.Content.Equals("AA") && !x.Key.Content.Equals("ZZ")).ToDictionary(x => x.Key, x => x.Value);
                if (portalPositions.Count == 0)
                    break;

                var initialPortal = portalPositions.First();
                portalPositions.Remove(initialPortal.Key);
                var endPortal = portalPositions.Single(x => x.Key.Content.Equals(initialPortal.Key.Content));
                portalPositions.Remove(endPortal.Key);

                var initialPortalPositions = initialPortal.Value.ToList();
                var endPortalPositions = endPortal.Value.ToList();
                for (var i = 0; i < initialPortalPositions.Count; i++)
                {
                    for (var j = 0; j < endPortalPositions.Count; j++)
                    {
                        graph.AddEdge(new Tuple<Position, Position>(initialPortalPositions[i], endPortalPositions[j]));
                    }
                }

                var valuesListsInitialPortal = graph.AdjacencyList.Where(x => x.Value.Contains(initialPortal.Key));
                var valuesListsEndPortal = graph.AdjacencyList.Where(x => x.Value.Contains(endPortal.Key));

                foreach (var node in valuesListsInitialPortal)
                {
                    node.Value.Replace(initialPortal.Value, initialPortal.Key);
                }

                foreach (var node in valuesListsInitialPortal)
                {
                    node.Value.Replace(initialPortal.Value, initialPortal.Key);
                }

                graph.RemoveVertex(initialPortal.Key);
                graph.RemoveVertex(endPortal.Key);
            }

            //graph.RemoveVertex(graph.AdjacencyList.First(x => x.Key.Content.Equals("A")).Key);
            //graph.RemoveVertex(graph.AdjacencyList.Last(x => x.Key.Content.Equals("Z")).Key);
        }

        private static void Replace(this HashSet<Position> set, HashSet<Position> toAdd, Position toRemove)
        {
            set.Remove(toRemove);
            foreach (var pos in toAdd)
            {
                set.Add(pos);
            }
        }

    }

    public class Position
    {
        public Position(int x, int y, string content)
        {
            X = x;
            Y = y;
            Content = content;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Content { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Position item))
            {
                return false;
            }

            return X == item.X && Y == item.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
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

    public class PositionsGraph : Graph<Position>
    {
        public KeyValuePair<Position, HashSet<Position>> GetVertex(Position vertex)
        {
            return AdjacencyList.FirstOrDefault(x => x.Key.X == vertex.X && x.Key.Y == vertex.Y);
        }

        public bool ContainsVertex(Position vertex)
        {
            return AdjacencyList.Any(x => x.Key.X == vertex.X && x.Key.Y == vertex.Y);
        }

        public void RemoveVertex(Position vertex)
        {
            AdjacencyList.Remove(GetVertex(vertex).Key);

            foreach (var list in AdjacencyList.Where(x => x.Value.Contains(vertex)).Select(x => x.Value))
            {
                list.Remove(vertex);
            }
        }

        public Position GetInitialVertex()
        {
            return AdjacencyList.Single(x => x.Key.Content.Equals("AA")).Key;
        }

        public Position GetFinalVertex()
        {
            return AdjacencyList.First(x => x.Key.Content.Equals("ZZ")).Key;
        }

        public List<Position> BFSShortestPath(Position source, Position dest)
        {
            var path = new Dictionary<Position, Position>();
            var distance = new Dictionary<Position, int>();
            foreach (var node in AdjacencyList.Keys)
            {
                distance[node] = -1;
            }
            distance[source] = 0;
            var q = new Queue<Position>();
            q.Enqueue(source);
            while (q.Count > 0)
            {
                var node = q.Dequeue();
                foreach (var adj in AdjacencyList[node].Where(n => distance[n] == -1))
                {
                    distance[adj] = distance[node] + 1;
                    path[adj] = node;
                    q.Enqueue(adj);
                }
            }
            var res = new List<Position>();
            if (!path.TryGetValue(dest, out Position existPosition))
            {
                return null;
            }

            var cur = dest;
            while (cur != source)
            {
                res.Add(cur);
                cur = path[cur];
            }
            res.Add(source);
            return res;
        }

        public int BFSShortestPathDistanceLevels(Position source, Position dest, int maxLevels)
        {
            var q = new Queue<(Position, int, int)>();
            var visited = new List<(Position, int)>();
            q.Enqueue((source, 0, 0));
            while (q.Count > 0)
            {
                var (node, level, distance) = q.Dequeue();

                if (node.Content.Equals("ZZ") && level < 1)
                    return distance;

                if (level > maxLevels)
                    continue;

                if (node.Content.Equals("ZZ") && level > 0)
                    continue;

                if (visited.Contains((node, level)))
                    continue;
                else
                    visited.Add((node, level));

                foreach (var adj in AdjacencyList[node])
                {
                    //var newHistory = history + $"{adj.X} {adj.Y} {adj.Content} {level}\n";
                    if (IsPortal(node))
                        q.Enqueue((adj, level, distance));
                    else
                        q.Enqueue((adj, level, distance + 1));
                }

                if (IsPortal(node))
                {
                    var twin = GetTwin(node);
                    if (IsInternalPortal(node))
                    {
                        q.Enqueue((twin, level + 1, distance));
                    }
                    else
                    {
                        if (level > 0)
                            q.Enqueue((twin, level - 1, distance));
                    }
                }
            }
            return 0;
        }

        private bool IsNormalEdge(Position start, Position end)
        {
            return start.X + 1 == end.X && start.Y == end.Y ||
                   start.X - 1 == end.X && start.Y == end.Y ||
                   start.X == end.X && start.Y + 1 == end.Y ||
                   start.X == end.X && start.Y - 1 == end.Y;
        }

        private bool IsPortal(Position position)
        {
            return position.Content.All(char.IsUpper) && !position.Content.Equals("AA") && !position.Content.Equals("ZZ");
        }

        private bool IsInternalPortal(Position position)
        {
            int minX = AdjacencyList.Keys.Min(x => x.X);
            int minY = AdjacencyList.Keys.Min(y => y.Y);
            int maxX = AdjacencyList.Keys.Max(x => x.X);
            int maxY = AdjacencyList.Keys.Max(y => y.Y);

            return position.X != minX && position.Y != minY && position.X != maxX && position.Y != maxY;
        }

        private Position GetTwin(Position position)
        {
            if (IsPortal(position))
            {
                return AdjacencyList.Keys.Single(pos => pos.Content.Equals(position.Content) && pos.X != position.X && pos.Y != position.Y);
            }
            else
            {
                throw new Exception("Should not be here");
            }
        }

        internal int GetMaxLevels()
        {
            const int minX = 1;
            const int minY = 1;
            int maxX = AdjacencyList.Keys.Max(x => x.X);
            int maxY = AdjacencyList.Keys.Max(y => y.Y);

            var list = AdjacencyList.Keys.Where(x => x.Content.All(char.IsUpper) && !(x.X == minX || x.Y == minY || x.X == maxX || x.Y == maxY)).ToList();
            return list.Count;
        }
    }
}
