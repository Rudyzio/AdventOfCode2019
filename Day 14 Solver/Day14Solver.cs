using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Day_14_Solver
{
    public static class Day14Solver
    {
        public const string ORE = "ORE";
        public const string FUEL = "FUEL";
        public const long TRILLION = 1000000000000;

        public static long Part1Solution(string[] input)
        {
            Dictionary<Chemical, List<Chemical>> chemicalOperations = ReadInput(input);
            long oreUsed = 0;
            OreNeeded(chemicalOperations, FUEL, 1, new Dictionary<string, long>(), ref oreUsed);
            return oreUsed;
        }

        public static long Part2Solution(string[] input)
        {
            Dictionary<Chemical, List<Chemical>> chemicalOperations = ReadInput(input);
            return Solve(chemicalOperations);
        }

        private static Dictionary<Chemical, List<Chemical>> ReadInput(string[] input)
        {
            var toReturn = new Dictionary<Chemical, List<Chemical>>();
            foreach (string line in input)
            {
                var toAdd = new List<Chemical>();
                var splitted = line.Split("=>");
                var leftSide = splitted[0].Trim();
                var result = splitted[1].Trim();
                foreach (var pair in leftSide.Split(","))
                {
                    toAdd.Add(new Chemical(int.Parse(pair.Trim().Split(" ")[0]), pair.Trim().Split(" ")[1]));
                }
                toReturn.Add(new Chemical(int.Parse(result.Trim().Split(" ")[0]), result.Trim().Split(" ")[1]), toAdd);
            }
            return toReturn;
        }

        private static void OreNeeded(Dictionary<Chemical, List<Chemical>> chemicalOperations, string chemicalName, long chemicalAmount, Dictionary<string, long> stock, ref long oreUsed)
        {
            stock.TryGetValue(chemicalName, out long storedAmount);
            stock[chemicalName] = Math.Max(storedAmount - chemicalAmount, 0);
            chemicalAmount = Math.Max(chemicalAmount - storedAmount, 0);

            if (chemicalAmount == 0)
                return;

            if (chemicalName.Equals(ORE))
            {
                oreUsed += chemicalAmount;
                return;
            }

            var chemicalOperation = chemicalOperations.First(x => x.Key.Name.Equals(chemicalName));
            Chemical chemNeeded = chemicalOperation.Key;
            long countRepetitions = (long)Math.Ceiling(chemicalAmount / (double)chemNeeded.Amount);
            countRepetitions = Math.Max(1, countRepetitions);

            var inputs = chemicalOperation.Value;

            foreach (var inp in inputs)
            {
                OreNeeded(chemicalOperations, inp.Name, inp.Amount * countRepetitions, stock, ref oreUsed);
            }

            long waste = (chemNeeded.Amount * countRepetitions) - chemicalAmount;
            if (waste > 0)
            {
                stock[chemicalName] += waste;
            }
        }

        public static long Solve(Dictionary<Chemical, List<Chemical>> chemicalOperations)
        {
            long scale = 100000;
            long guess = 1;

            long result = 0;
            while (scale > 1)
            {
                result = Guess(chemicalOperations, guess, scale);
                guess = result - scale;
                scale /= 10;
            }

            guess = result - 10;
            result = Guess(chemicalOperations, guess, scale);

            return result - 1;
        }

        private static long Guess(Dictionary<Chemical, List<Chemical>> chemicalOperations, long guess, long scale)
        {
            long fuelDone = 0;
            Dictionary<string, long> stock = new Dictionary<string, long>
            {
                { ORE, TRILLION }
            };
            Build(chemicalOperations, stock, guess, FUEL, ref fuelDone);

            while (!Build(chemicalOperations, stock, scale, FUEL, ref fuelDone)) ;

            return fuelDone;
        }

        private static bool Build(Dictionary<Chemical, List<Chemical>> chemicalOperations, Dictionary<string, long> stock, long chemicalAmount, string chemicalName, ref long fuels)
        {
            if (chemicalName.Equals(FUEL))
                fuels += chemicalAmount;

            stock.TryGetValue(chemicalName, out long storedAmount);
            stock[chemicalName] = Math.Max(storedAmount - chemicalAmount, 0);
            chemicalAmount = Math.Max(chemicalAmount - storedAmount, 0);

            if (chemicalAmount == 0)
                return false;

            if (chemicalName.Equals(ORE))
            {
                return true;
            }

            var chemicalOperation = chemicalOperations.First(x => x.Key.Name.Equals(chemicalName));
            Chemical chemNeeded = chemicalOperation.Key;
            long countRepetitions = (long)Math.Ceiling(chemicalAmount / (double)chemNeeded.Amount);
            countRepetitions = Math.Max(1, countRepetitions);

            var inputs = chemicalOperation.Value;

            var boolReturn = false;
            foreach (var inp in inputs)
            {
                boolReturn = Build(chemicalOperations, stock, inp.Amount * countRepetitions, inp.Name, ref fuels);
            }

            long waste = (chemNeeded.Amount * countRepetitions) - chemicalAmount;
            if (waste > 0)
            {
                stock[chemicalName] += waste;
            }
            return boolReturn;
        }

    }

    public class Chemical
    {
        public Chemical(int amount, string name)
        {
            Amount = amount;
            Name = name.Trim();
        }

        public int Amount { get; set; }

        public string Name { get; set; }
    }
}
