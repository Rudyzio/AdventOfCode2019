using Common;
using System.Collections.Generic;
using System.Linq;

namespace Day_23_Solver
{
    public static class Day23Solver
    {
        public static long Part1Solution(long[] input)
        {
            List<IntCodeProgram> intCodePrograms = new List<IntCodeProgram>();
            Dictionary<long, Queue<Packet>> PacketsForIntCode = new Dictionary<long, Queue<Packet>>();
            intCodePrograms.InitPrograms(input);
            PacketsForIntCode.InitPacketStorage();

            while (true)
            {
                for (var i = 0; i < intCodePrograms.Count; i++)
                {
                    // Get the packets for current IntCodeProgram and enqueue them on Input
                    while (PacketsForIntCode[i].Count > 0)
                    {
                        var packet = PacketsForIntCode[i].Dequeue();
                        intCodePrograms[i].Input.Enqueue(packet.X);
                        intCodePrograms[i].Input.Enqueue(packet.Y);
                    }

                    // Run the current IntCodeProgram
                    var halt = intCodePrograms[i].Run();
                    if (halt == Halt.NeedInput)
                    {
                        intCodePrograms[i].Input.Enqueue(-1);
                    }

                    // Get the outputs
                    while (intCodePrograms[i].Output.Count > 0)
                    {
                        var address = intCodePrograms[i].Output.Dequeue();
                        var x = intCodePrograms[i].Output.Dequeue();
                        var y = intCodePrograms[i].Output.Dequeue();

                        // Found needed address
                        if (address == 255)
                            return y;

                        // Make a packet to the destination
                        var packet = new Packet(x, y);
                        PacketsForIntCode[address].Enqueue(packet);
                    }
                }
            }
        }

        public static long Part2Solution(long[] input)
        {
            List<IntCodeProgram> intCodePrograms = new List<IntCodeProgram>();
            Dictionary<long, Queue<Packet>> PacketsForIntCode = new Dictionary<long, Queue<Packet>>();
            Dictionary<long, Halt> intCodeProgramStates = new Dictionary<long, Halt>();
            Packet lastPacket = null;
            List<long> deliveredValues = new List<long>();

            intCodePrograms.InitPrograms(input);
            PacketsForIntCode.InitPacketStorage();

            while (true)
            {
                for (var i = 0; i < intCodePrograms.Count; i++)
                {
                    // Get the packets for current IntCodeProgram and enqueue them on Input
                    while (PacketsForIntCode[i].Count > 0)
                    {
                        var packet = PacketsForIntCode[i].Dequeue();
                        intCodePrograms[i].Input.Enqueue(packet.X);
                        intCodePrograms[i].Input.Enqueue(packet.Y);
                    }

                    // Run the current IntCodeProgram
                    var halt = intCodePrograms[i].Run();
                    if (halt == Halt.NeedInput)
                    {
                        intCodePrograms[i].Input.Enqueue(-1);
                    }
                    intCodeProgramStates[i] = halt;

                    // Get the outputs
                    while (intCodePrograms[i].Output.Count > 0)
                    {
                        var address = intCodePrograms[i].Output.Dequeue();
                        var x = intCodePrograms[i].Output.Dequeue();
                        var y = intCodePrograms[i].Output.Dequeue();

                        // Found needed address
                        if (address == 255)
                        {
                            lastPacket = new Packet(x, y);
                        }
                        else
                        {
                            // Make a packet to the destination
                            var packet = new Packet(x, y);
                            PacketsForIntCode[address].Enqueue(packet);
                        }
                    }
                }

                // Check idle state
                if (PacketsForIntCode.All(x => x.Value.Count == 0) && intCodeProgramStates.All(x => x.Value == Halt.NeedInput) && lastPacket != null)
                {
                    // Send only the last packet to address 0
                    intCodePrograms[0].Input.Enqueue(lastPacket.X);
                    intCodePrograms[0].Input.Enqueue(lastPacket.Y);
                    if (deliveredValues.Contains(lastPacket.Y))
                        return lastPacket.Y;
                    else
                        deliveredValues.Add(lastPacket.Y);
                }
            }
        }

        private static void InitPrograms(this List<IntCodeProgram> intCodePrograms, long[] input)
        {
            for (var i = 0; i < 50; i++)
            {
                var intCodeProgram = new IntCodeProgram(input);
                intCodeProgram.Input.Enqueue(i);
                intCodeProgram.Run();
                intCodePrograms.Add(intCodeProgram);
            }
        }

        private static void InitPacketStorage(this Dictionary<long, Queue<Packet>> packetsForIntCode)
        {
            for (var i = 0; i < 50; i++)
            {
                packetsForIntCode.Add(i, new Queue<Packet>());
            }
        }
    }

    public class Packet
    {
        public Packet(long x, long y)
        {
            X = x;
            Y = y;
        }
        public long X { get; set; }

        public long Y { get; set; }
    }
}
