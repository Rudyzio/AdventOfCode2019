using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day_22_Solver
{
    public static class Day22Solver
    {
        public static long Part1Solution(string[] input, int deckSize, int numberPosition)
        {
            var deck = InitDeck(deckSize);
            foreach (var line in input)
            {
                var splitted = line.Split(" ");
                if (splitted[0].Equals("deal"))
                {
                    if (splitted[1].Equals("into"))
                    {
                        DealIntoNewStack(deck);
                    }
                    if (splitted[1].Equals("with"))
                    {
                        var increment = int.Parse(splitted[3]);
                        deck = Increment(deck, increment);
                    }
                }

                if (splitted[0].Equals("cut"))
                {
                    var cut = int.Parse(splitted[1]);
                    CutCards(deck, cut);
                }
            }

            return SearchForNumberPosition(deck, numberPosition);
        }

        public static BigInteger Part2Solution(string[] input, BigInteger deckSize, BigInteger shuffleAmount, int cardPosition)
        {
            BigInteger offset = 0;
            BigInteger increment = 1;
            foreach (var line in input)
            {
                var splitted = line.Split(" ");
                if (splitted[0].Equals("deal"))
                {
                    if (splitted[1].Equals("into"))
                    {
                        DealIntoNewStackArithmetic(ref offset, ref increment);
                    }
                    if (splitted[1].Equals("with"))
                    {
                        var toIncrement = int.Parse(splitted[3]);
                        IncrementArithmetic(ref increment, toIncrement, deckSize);
                    }
                }

                if (splitted[0].Equals("cut"))
                {
                    var cut = int.Parse(splitted[1]);
                    CutCardsArithmetic(ref offset, ref increment, cut);
                }

                increment = increment.Mod(deckSize);
                offset = offset.Mod(deckSize);
            }

            (BigInteger incrementSequence, BigInteger offsetSequence) = GetSequence(shuffleAmount, increment, offset, deckSize);

            return (offsetSequence + (cardPosition * incrementSequence)) % deckSize;
        }

        private static List<long> InitDeck(long deckSize)
        {
            var toReturn = new List<long>();
            for (var i = 0; i < deckSize; i++)
            {
                toReturn.Add(i);
            }
            return toReturn;
        }

        private static void DealIntoNewStack(List<long> toTransform)
        {
            toTransform.Reverse();
        }

        private static void DealIntoNewStackArithmetic(ref BigInteger offset, ref BigInteger increment)
        {
            increment *= -1;
            offset += increment;
        }

        private static void CutCards(List<long> toTransform, int N)
        {
            var taken = new List<long>();
            var skipped = new List<long>();
            if (N > 0)
            {
                taken = toTransform.Take(N).ToList();
                skipped = toTransform.Skip(N).Take(toTransform.Count - N).ToList();
            }
            else
            {
                N = Math.Abs(N);
                skipped = toTransform.Skip(toTransform.Count - N).Take(N).ToList();
                taken = toTransform.Take(toTransform.Count - N).ToList();
            }

            toTransform.Clear();
            toTransform.AddRange(skipped);
            toTransform.AddRange(taken);
        }

        private static void CutCardsArithmetic(ref BigInteger offset, ref BigInteger increment, int n)
        {
            offset += increment * n;
        }

        private static List<long> Increment(List<long> toTransform, int increment)
        {
            var times = toTransform.Count;
            var currentIndex = 0;
            var incremented = 0;
            var table = new long[toTransform.Count];
            while (currentIndex < times)
            {
                if (incremented >= toTransform.Count)
                    incremented -= toTransform.Count;
                table[incremented] = toTransform[currentIndex];
                currentIndex++;
                incremented += increment;
            }
            return table.ToList();
        }

        private static void IncrementArithmetic(ref BigInteger increment, int n, BigInteger deckSize)
        {
            increment *= new BigInteger(n).Inv(deckSize);
        }

        private static int SearchForNumberPosition(List<long> deck, int numberPosition)
        {
            for (var i = 0; i < deck.Count; i++)
            {
                if (deck[i] == numberPosition)
                    return i;
            }

            throw new Exception("Number not found");
        }

        // Helpers

        private static BigInteger Mod(this BigInteger x, BigInteger m)
        {
            return ((x % m) + m) % m;
        }

        private static BigInteger Inv(this BigInteger num, BigInteger size)
        {
            return BigInteger.ModPow(num, size - 2, size);
        }

        private static (BigInteger increment, BigInteger offset) GetSequence(this BigInteger iterations, BigInteger inc_mul, BigInteger offset_diff, BigInteger size)
        {
            var increment = BigInteger.ModPow(inc_mul, iterations, size);

            var offset = offset_diff * (1 - increment) * ((1 - inc_mul) % size).Inv(size);

            offset %= size;

            return (increment, offset);
        }
    }
}
