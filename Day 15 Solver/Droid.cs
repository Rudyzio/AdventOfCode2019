using Common;

namespace Day_15_Solver
{
    public class Droid
    {
        private IntCodeProgram intCodeProgram;

        public Droid(IntCodeProgram program)
        {
            intCodeProgram = program;
        }

        public Answer Move(Command command)
        {
            intCodeProgram.Input.Enqueue((long)command);
            intCodeProgram.Run();
            return (Answer)intCodeProgram.Output.Dequeue();
        }
    }

    public class Position
    {
        public Position(int x, int y, Answer content)
        {
            X = x;
            Y = y;
            Content = content;
            NorthTried = false;
            SouthTried = false;
            EastTried = false;
            WestTried = false;
            HasOxygen = false;
            Distance = 0;
            Visited = false;
            Level = 0;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Answer Content { get; set; }
        public bool Visited { get; set; }
        public bool NorthTried { get; set; }
        public bool SouthTried { get; set; }
        public bool EastTried { get; set; }
        public bool WestTried { get; set; }
        public bool HasOxygen { get; set; }
        public long Distance { get; set; }
        public int Level { get; set; }
    }

    public enum Command
    {
        North = 1,
        South = 2,
        West = 3,
        East = 4,
        NoCommand = 99
    }

    public enum Answer
    {
        Wall = 0,
        Allowed = 1,
        Success = 2
    }
}
