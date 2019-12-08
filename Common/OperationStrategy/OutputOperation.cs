using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class OutputOperation : OperationStrategy
    {
        private readonly Queue<int> _output;

        public OutputOperation(Queue<int> output)
        {
            _output = output;
        }

        public override void Execute(int[] input, ref int instructionPointer, List<ParameterMode> paramModes)
        {
            int param = input[instructionPointer + 1];
            ParameterMode paramMode = paramModes[0];

            if (paramMode == ParameterMode.Immediate)
            {
                _output.Enqueue(param);
            }
            else
            {
                _output.Enqueue(input[param]);
            }

            instructionPointer += 2;
        }
    }
}
