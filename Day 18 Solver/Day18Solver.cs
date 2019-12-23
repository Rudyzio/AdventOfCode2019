using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_18_Solver
{
    public static class Day18Solver
    {
        public static int Part1Solution(string[] input)
        {
            var reachableKeys = new Dictionary<Position, List<Key>>
            {
                { ReadInput(input).GetEntrance(), GetKeysDistances(ReadInput(input), ReadInput(input).GetEntrance(), string.Empty) }
            };

            foreach (var keyPosition in ReadInput(input).GetAllKeys())
            {
                reachableKeys.Add(keyPosition, GetKeysDistances(ReadInput(input), keyPosition, keyPosition.Content.ToString()));
            }

            return CollectKeys(reachableKeys, "@");
        }


        public static int Part2Solution(string[] input)
        {
            var entrances = ReadInput(input).GetEntrances();
            var reachableKeys = new Dictionary<Position, List<Key>>();

            foreach (var entrance in entrances)
            {
                reachableKeys.Add(entrance, GetKeysDistances(ReadInput(input), entrance, string.Empty));
            }

            foreach (var keyPosition in ReadInput(input).GetAllKeys())
            {
                reachableKeys.Add(keyPosition, GetKeysDistances(ReadInput(input), keyPosition, keyPosition.Content.ToString()));
            }

            var entrs = reachableKeys.Where(x => x.Key.Content.ToString().Equals("@")).Select(x => x.Key).ToList();
            int robotNumber = 1;
            foreach (var entrance in entrs)
            {
                entrance.Content = (char)robotNumber;
                robotNumber++;
            }

            return BigCollectKeys(reachableKeys, entrs, ReadInput(input).GetAllKeys().Count);
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
                            break;
                        case '.':
                            allPositions.Add(new Position(x, y, ' '));
                            break;
                        default:
                            allPositions.Add(new Position(x, y, character));
                            break;

                    }
                    x++;
                }
                x = 0;
                y++;
            }

            foreach (var position in allPositions)
            {
                positions.AddVertex(position);
                var adjacentPositions = GetAdjacentPositions(allPositions, position);
                foreach (var adjacentPosition in adjacentPositions)
                {
                    positions.AddEdge(new Tuple<Position, Position>(position, adjacentPosition));
                }
            }

            return positions;
        }

        private static List<Position> GetAdjacentPositions(List<Position> positions, Position position)
        {
            return positions.Where(pos => pos.X + 1 == position.X && pos.Y == position.Y ||
                                          pos.X - 1 == position.X && pos.Y == position.Y ||
                                          pos.X == position.X && pos.Y + 1 == position.Y ||
                                          pos.X == position.X && pos.Y - 1 == position.Y).ToList();
        }

        private static List<Key> GetKeysDistances(PositionsGraph graph, Position position, string toExclude)
        {
            var toReturn = new List<Key>();

            var allKeys = graph.GetAllKeys().Where(x => !x.Content.ToString().Equals(toExclude)).ToList();
            foreach (var key in allKeys)
            {
                var path = graph.BFSShortestPath(position, key);
                if (path == null)
                {
                    continue;
                }
                var doors = path.Where(x => char.IsUpper(x.Content)).ToList();
                var keyToCreate = new Key(key.X, key.Y, key.Content, path.Count - 1, doors);
                toReturn.Add(keyToCreate);
            }

            return toReturn;
        }

        private static int CollectKeys(Dictionary<Position, List<Key>> keysMap, string content)
        {
            var currentMinimum = int.MaxValue;
            Dictionary<State, int> memStates = new Dictionary<State, int>();
            Stack<State> states = new Stack<State>();
            states.Push(new State
            {
                Distance = 0,
                Positions = keysMap.ToDictionary(e => e.Key, e => e.Value.ToList()),
                CurrentPosition = keysMap.Single(x => x.Key.Content.ToString().Equals(content)).Key,
                KeysCollected = new List<string>()
            });

            while (states.Count > 0)
            {
                var state = states.Pop();

                if (memStates.TryGetValue(state, out var distance))
                {
                    if (distance <= state.Distance)
                        continue;
                    else
                        memStates[state] = state.Distance;
                }
                else
                {
                    memStates.Add(state, state.Distance);
                }

                if (state.Positions.Count < 2)
                {
                    state.Debugging += $"Total Distance is {state.Distance} \n";
                    Console.WriteLine(state.Debugging);
                    currentMinimum = memStates[state] < currentMinimum ? memStates[state] : currentMinimum;
                }
                else
                {
                    var pair = state.Positions.Single(x => x.Key.Content.ToString().Equals(state.CurrentPosition.Content.ToString()));
                    var noObstacleKeys = pair.Value.Where(x => x.Obstacles.Count == 0).ToList();

                    foreach (var noObstacleKey in noObstacleKeys)
                    {
                        var newState = new State
                        {
                            Distance = state.Distance + noObstacleKey.Distance,
                            CurrentPosition = noObstacleKey,
                            KeysCollected = state.KeysCollected.ToList(),
                            Positions = state.Positions.ToDictionary(e => e.Key, e => e.Value.Select(x => new Key(x.X, x.Y, x.Content, x.Distance, x.Obstacles.ToList())).ToList()),
                            Debugging = state.Debugging
                        };
                        newState.KeysCollected.Add(noObstacleKey.Content.ToString());

                        newState.Positions.Remove(pair.Key);
                        newState.Debugging += $"Removed key {noObstacleKey.Content} with distance {noObstacleKey.Distance} \n";
                        var keys = new List<Position>(newState.Positions.Keys);
                        foreach (var p in keys)
                        {
                            foreach (var v in newState.Positions[p])
                            {
                                v.Obstacles = v.Obstacles.Where(x => !x.Content.ToString().Equals(noObstacleKey.Content.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
                            }
                            newState.Positions[p] = newState.Positions[p].Where(x => !x.Content.ToString().Equals(noObstacleKey.Content.ToString())).ToList();
                        }

                        states.Push(newState);
                    }
                }
            }

            return currentMinimum;
        }

        private static int BigCollectKeys(Dictionary<Position, List<Key>> keysMap, List<Position> startingPositions, int totalKeys)
        {
            var currentMinimum = 2218;
            Dictionary<BigState, int> memStates = new Dictionary<BigState, int>();
            Queue<BigState> states = new Queue<BigState>();
            states.Enqueue(new BigState
            {
                Distance = 0,
                Positions = keysMap.ToDictionary(e => e.Key, e => e.Value.ToList()),
                CurrentPositions = startingPositions.ToList(),
                KeysCollected = new List<string>()
            });

            while (states.Count > 0)
            {
                var state = states.Dequeue();

                if (memStates.TryGetValue(state, out var distance))
                {
                    if (distance <= state.Distance)
                        continue;
                    else
                        memStates[state] = state.Distance;
                }
                else
                {
                    memStates.Add(state, state.Distance);
                }

                if (totalKeys == state.KeysCollected.Count)
                {
                    state.Debugging += $"Total Distance is {state.Distance} \n";
                    Console.WriteLine(state.Debugging);
                    currentMinimum = memStates[state] < currentMinimum ? memStates[state] : currentMinimum;
                }
                else
                {
                    foreach (var currentPosition in state.CurrentPositions)
                    {
                        var pair = state.Positions.Single(x => x.Key.Content.ToString().Equals(currentPosition.Content.ToString()));
                        var noObstacleKeys = pair.Value.Where(x => x.Obstacles.Count == 0).ToList();

                        if (noObstacleKeys.Count == 0)
                        {
                            //newState.CurrentPositions.Add(currentPosition);
                            continue;
                        }

                        foreach (var noObstacleKey in noObstacleKeys)
                        {
                            if (state.Distance + noObstacleKey.Distance > currentMinimum)
                            {
                                continue;
                            }

                            var newPositions = state.Positions.ToDictionary(e => e.Key, e => e.Value.Select(x => new Key(x.X, x.Y, x.Content, x.Distance, x.Obstacles.ToList())).ToList());
                            var newCurrentPositions = state.CurrentPositions.ToList();
                            var newKeysCollected = state.KeysCollected.ToList();
                            newCurrentPositions.Remove(currentPosition);
                            newCurrentPositions.Add(noObstacleKey);

                            newKeysCollected.Add(noObstacleKey.Content.ToString());

                            newPositions.Remove(pair.Key);
                            var keys = new List<Position>(newPositions.Keys);
                            foreach (var p in keys)
                            {
                                foreach (var v in newPositions[p])
                                {
                                    v.Obstacles = v.Obstacles.Where(x => !x.Content.ToString().Equals(noObstacleKey.Content.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
                                }
                                newPositions[p] = newPositions[p].Where(x => !x.Content.ToString().Equals(noObstacleKey.Content.ToString())).ToList();
                            }

                            states.Enqueue(new BigState
                            {
                                Distance = state.Distance + noObstacleKey.Distance,
                                CurrentPositions = newCurrentPositions,
                                Debugging = state.Debugging + $"Removed key {noObstacleKey.Content} with distance {noObstacleKey.Distance} \n",
                                Positions = newPositions,
                                KeysCollected = newKeysCollected
                            });
                        }
                    }
                }
            }

            return currentMinimum;
        }
    }

    public class Position
    {
        public Position(int x, int y, char content)
        {
            X = x;
            Y = y;
            Content = content;
            Distance = 0;
        }

        public Position(int x, int y, char content, int distance)
        {
            X = x;
            Y = y;
            Content = content;
            Distance = distance;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public char Content { get; set; }
        public int Distance { get; set; }

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

    public class Key : Position
    {
        public Key(int x, int y, char content, int distance, List<Position> obstacles) : base(x, y, content, distance)
        {
            Obstacles = obstacles.ToList();
        }

        public List<Position> Obstacles { get; set; }
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
        }

        public bool ContainsLowerCaseLetters()
        {
            return AdjacencyList.Any(x => !char.IsWhiteSpace(x.Key.Content) && char.IsLower(x.Key.Content));
        }

        public Position GetEntrance()
        {
            return AdjacencyList.Single(x => x.Key.Content.Equals('@')).Key;
        }

        public List<Position> GetEntrances()
        {
            return AdjacencyList.Where(x => x.Key.Content.Equals('@')).Select(x => x.Key).ToList();
        }

        public List<Key> GetAllKeys()
        {
            return AdjacencyList.Where(x => char.IsLower(x.Key.Content)).Select(x => new Key(x.Key.X, x.Key.Y, x.Key.Content, x.Key.Distance, new List<Position>())).ToList();
        }

        public List<Position> GetAllDoors()
        {
            return AdjacencyList.Where(x => char.IsUpper(x.Key.Content)).Select(x => x.Key).ToList();
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
    }

    public class State
    {
        public Dictionary<Position, List<Key>> Positions { get; set; }
        public List<string> KeysCollected { get; set; }
        public int Distance { get; set; }
        public Position CurrentPosition { get; set; }
        public string Debugging { get; set; } = string.Empty;

        public override bool Equals(object obj)
        {
            if (!(obj is State item))
            {
                return false;
            }

            return KeysCollected.Count == item.KeysCollected.Count && !KeysCollected.Except(item.KeysCollected).Any();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CurrentPosition.X, CurrentPosition.Y);
        }
    }

    public class BigState
    {
        public Dictionary<Position, List<Key>> Positions { get; set; }
        public List<string> KeysCollected { get; set; }
        public int Distance { get; set; }
        public List<Position> CurrentPositions { get; set; }
        public string Debugging { get; set; } = string.Empty;

        public override bool Equals(object obj)
        {
            if (!(obj is BigState item))
            {
                return false;
            }

            return KeysCollected.Count == item.KeysCollected.Count && !KeysCollected.Except(item.KeysCollected).Any()
                && CurrentPositions.Count == item.CurrentPositions.Count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CurrentPositions.First().X, CurrentPositions.First().Y);
        }
    }
}
