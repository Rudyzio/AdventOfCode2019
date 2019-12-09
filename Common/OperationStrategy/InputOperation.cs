using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class InputOperation : OperationStrategy
    {
        private readonly Queue<long> _input;

        public InputOperation(Queue<long> input)
        {
            _input = input;
        }

        public override void Execute(ref long[] input, ref long instructionPointer, List<ParameterMode> paramModes, ref long relativeBase)
        {
            long param = input[instructionPointer + 1];
            long address = GetAddress(param, paramModes[0], ref input, ref relativeBase);
            input[address] = _input.Dequeue();

            instructionPointer += 2;
        }
    }
}
