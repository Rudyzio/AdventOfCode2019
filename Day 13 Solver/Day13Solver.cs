using Common;
using System.Collections.Generic;
using System.Linq;

namespace Day_13_Solver
{
    public static class Day13Solver
    {
        public static long Part1Solution(long[] input)
        {
            List<GridPosition> positions = new List<GridPosition>();
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            intCodeProgram.Run();

            ProcessIntCodeOutput(positions, intCodeProgram);
            return positions.Count(position => position.Tile == Tile.Block);
        }

        public static long Part2Solution(long[] input)
        {
            long currentScore = 0;
            long currentBlockTiles = -1;
            input[0] = 2;
            List<GridPosition> positions = new List<GridPosition>();
            IntCodeProgram intCodeProgram = new IntCodeProgram(input);

            GridPosition horizontalPaddlePosition = new GridPosition(0, 0, Tile.HorizontalPaddle);
            GridPosition ballPosition = new GridPosition(0, 0, Tile.Ball);

            while (currentBlockTiles != 0)
            {
                intCodeProgram.Run();

                (currentScore, ballPosition, horizontalPaddlePosition) = ProcessIntCodeOutput(positions, intCodeProgram);

                ProvideInput(intCodeProgram, ballPosition, horizontalPaddlePosition);

                currentBlockTiles = positions.Count(pos => pos.Tile == Tile.Block);
            }

            return currentScore;
        }

        private static (long, GridPosition, GridPosition) ProcessIntCodeOutput(List<GridPosition> positions, IntCodeProgram intCodeProgram)
        {
            long currentScore = 0;
            GridPosition horizontalPaddlePosition = new GridPosition(0, 0, Tile.HorizontalPaddle);
            GridPosition ballPosition = new GridPosition(0, 0, Tile.Ball);

            while (intCodeProgram.Output.Count > 0)
            {
                var x = intCodeProgram.Output.Dequeue();
                var y = intCodeProgram.Output.Dequeue();
                var tile = intCodeProgram.Output.Dequeue();

                // Specifies output of current score
                if (x == -1 && y == 0)
                {
                    currentScore = tile;
                    continue;
                }

                var currentPosition = positions.SingleOrDefault(pos => pos.X == x && pos.Y == y);
                if (currentPosition == null)
                {
                    positions.Add(new GridPosition(x, y, (Tile)tile));
                }
                else
                {
                    currentPosition.Tile = (Tile)tile;
                }

                if ((Tile)tile == Tile.HorizontalPaddle)
                {
                    horizontalPaddlePosition.X = x;
                    horizontalPaddlePosition.Y = y;
                }

                if ((Tile)tile == Tile.Ball)
                {
                    ballPosition.X = x;
                    ballPosition.Y = y;
                }
            }

            return (currentScore, ballPosition, horizontalPaddlePosition);
        }

        private static void ProvideInput(IntCodeProgram intCodeProgram, GridPosition ballPosition, GridPosition horizontalPaddlePosition) 
        {
            if (ballPosition.X < horizontalPaddlePosition.X)
            {
                intCodeProgram.Input.Enqueue(-1);
            }
            if (ballPosition.X > horizontalPaddlePosition.X)
            {
                intCodeProgram.Input.Enqueue(1);
            }
            if (ballPosition.X == horizontalPaddlePosition.X)
            {
                intCodeProgram.Input.Enqueue(0);
            }
        }
    }

    public class GridPosition
    {
        public GridPosition(long x, long y, Tile tile)
        {
            X = x;
            Y = y;
            Tile = tile;
        }

        public long X { get; set; }

        public long Y { get; set; }

        public Tile Tile { get; set; }
    }

    public enum Tile
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        HorizontalPaddle = 3,
        Ball = 4
    }
}
