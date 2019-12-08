using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_6_Solver
{
    public static class Day6Solver
    {
        public static int Part1Solution(List<KeyValuePair<string, string>> pairs)
        {
            int counter = 0;
            var visitedNodes = new List<string>();
            foreach (var pair in pairs)
            {
                if (!visitedNodes.Contains(pair.Key))
                {
                    DepthFirstSearch(pairs, ref counter, pair.Key);
                    visitedNodes.Add(pair.Key);
                }
            }
            return counter;
        }

        public static int Part2Solution(List<KeyValuePair<string, string>> pairs)
        {
            const string YOU = "YOU";
            const string SAN = "SAN";

            Dictionary<string, List<string>> adjacencyList = FillAdjacencyList(pairs);

            Dictionary<string, int> meParentDistances = GetParentDistances(YOU, adjacencyList);
            Dictionary<string, int> sanParentDistances = GetParentDistances(SAN, adjacencyList);

            var common = meParentDistances.Keys.Intersect(sanParentDistances.Keys);

            int minDistance = common.Min(c =>
            {
                int meToCommon = meParentDistances[c];
                int sanToCommon = sanParentDistances[c];

                return meToCommon + sanToCommon;
            });

            return minDistance;
        }

        private static void DepthFirstSearch(List<KeyValuePair<string, string>> pairs, ref int counter, string planet)
        {
            var orbitsAroundIt = pairs.Where(kvp => kvp.Key.Equals(planet));
            if (!orbitsAroundIt.Any())
            {
                return;
            }
            foreach (var orbit in orbitsAroundIt)
            {
                counter++;
                DepthFirstSearch(pairs, ref counter, orbit.Value);
            }
        }

        private static Dictionary<string, List<string>> FillAdjacencyList(List<KeyValuePair<string, string>> pairs)
        {
            var toReturn = new Dictionary<string, List<string>>();
            var visitedNodes = new List<string>();
            foreach (var pair in pairs)
            {
                if (!visitedNodes.Contains(pair.Key))
                {
                    var children = pairs.Where(x => x.Key.Equals(pair.Key)).Select(x => x.Value).ToList();
                    toReturn.Add(pair.Key, children);
                    visitedNodes.Add(pair.Key);
                }
            }

            return toReturn;
        }

        private static Dictionary<string, int> GetParentDistances(string reference, Dictionary<string, List<string>> adjacencyList)
        {
            Dictionary<string, int> parentDistances = new Dictionary<string, int>();
            int i = 0;

            string parentReference = reference;

            while (parentReference != null)
            {
                parentReference = GetParent(parentReference, adjacencyList);

                if (parentReference != null)
                {
                    parentDistances.Add(parentReference, i);
                }

                i++;
            }

            return parentDistances;
        }

        private static string GetParent(string reference, Dictionary<string, List<string>> adjacencyList)
        {
            var parent = adjacencyList.Where(x => x.Value.Contains(reference));
            if (!parent.Any())
            {
                return null;
            }

            return parent.Single().Key;
        }
    }
}
