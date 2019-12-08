using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class InputOperation : OperationStrategy
    {
        private readonly Queue<int> _input;

        public InputOperation(Queue<int> input)
        {
            _input = input;
        }

        public override void Execute(int[] input, ref int instructionPointer, List<ParameterMode> paramModes)
        {
            int param = input[instructionPointer + 1];
            input[param] = _input.Dequeue();

            instructionPointer += 2;
        }
    }
}
