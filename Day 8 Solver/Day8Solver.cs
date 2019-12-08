using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_8_Solver
{
    public static class Day8Solver
    {
        public static string Part1Solution(List<int> input, int wide, int tall)
        {
            List<List<int>> layers = new List<List<int>>();

            while (input.Count > 0)
            {
                layers.Add(GetLayer(input, wide, tall));
            }

            int targetLayerIndex = 0;
            int? currentNumberZeros = null;
            for (var i = 0; i < layers.Count; i++)
            {
                int nZeros = layers[i].Count(x => x == 0);
                if (currentNumberZeros == null || nZeros < currentNumberZeros)
                {
                    currentNumberZeros = nZeros;
                    targetLayerIndex = i;
                }
            }

            return (layers[targetLayerIndex].Count(x => x == 1) * layers[targetLayerIndex].Count(x => x == 2)).ToString();
        }

        public static string Part2Solution(List<int> input, int wide, int tall)
        {
            List<List<int>> image = new List<List<int>>();

            while (input.Count > 0)
            {
                image.Add(GetLayer(input, wide, tall));
            }

            int[] outPut = new int[image.Sum(x => x.Count)];

            for (int y = 0; y < tall; y++)
            {
                for (int x = 0; x < wide; x++)
                {
                    int arrayOffSet = x + (y * wide);
                    foreach (var layer in image)
                    {
                        int c = layer[arrayOffSet];
                        if ((Colors)c != Colors.Transparent)
                        {
                            outPut[arrayOffSet] = c;
                            break;
                        }
                    }
                }
            }

            var output = string.Empty;
            for (int y = 0; y < tall; y++)
            {
                StringBuilder row = new StringBuilder();
                for (int x = 0; x < wide; x++)
                {
                    int arrayOffSet = x + (y * wide);

                    if (outPut[arrayOffSet] == 1)
                    {
                        output += "#";
                    }
                    else
                    {
                        output += " ";
                    }
                }
            }
            return output;
            // Add new line in the end of each 25 (wide) characters and you get the correct word
        }

        private static List<int> GetLayer(List<int> input, int wide, int tall)
        {
            var toReturn = input.Take(wide * tall).ToList();
            input.RemoveRange(0, wide * tall);
            return toReturn;
        }
    }

    public enum Colors
    {
        Black = 0,
        White = 1,
        Transparent = 2
    }
}
