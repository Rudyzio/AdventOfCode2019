using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class MultiplyOperation : OperationStrategy
    {
        public override void Execute(int[] input, ref int instructionPointer, List<ParameterMode> paramModes)
        {
            int firstParam = input[instructionPointer + 1];
            int secondParam = input[instructionPointer + 2];
            int outputPosition = input[instructionPointer + 3];

            int firstValue = GetParamValue(firstParam, paramModes[0], input);
            int secondValue = GetParamValue(secondParam, paramModes[1], input);

            input[outputPosition] = firstValue * secondValue;

            instructionPointer += 4;
        }
    }
}
