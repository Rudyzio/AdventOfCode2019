using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class OutputOperation : OperationStrategy
    {
        private readonly Queue<long> _output;

        public OutputOperation(Queue<long> output)
        {
            _output = output;
        }

        public override void Execute(ref long[] input, ref long instructionPolonger, List<ParameterMode> paramModes, ref long relativeBase)
        {
            long param = input[instructionPolonger + 1];
            ParameterMode paramMode = paramModes[0];

            switch(paramMode)
            {
                case ParameterMode.Immediate:
                    _output.Enqueue(param);
                    break;
                case ParameterMode.Position:
                    CheckIndexExistAndResizeIfNeeded(ref input, param);
                    _output.Enqueue(input[param]);
                    break;
                case ParameterMode.Relative:
                    CheckIndexExistAndResizeIfNeeded(ref input, relativeBase + param);
                    _output.Enqueue(input[relativeBase + param]);
                    break;
            }

            instructionPolonger += 2;
        }
    }
}
